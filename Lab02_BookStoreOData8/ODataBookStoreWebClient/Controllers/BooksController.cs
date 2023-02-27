using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using ODataBookStore.DTOs;
using ODataBookStore.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ODataBookStoreWebClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient client = null;
        private string BookApiUrl = "";
        private string PublisherApiUrl = "";

        public BooksController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = "https://localhost:44355/odata/Books";
            PublisherApiUrl = "https://localhost:44355/odata/Publishers";
        }

        public async Task<IActionResult> Index(string? search)
        {
            if(search != null)
            {
                BookApiUrl =  BookApiUrl + "?$filter=contains(Title, '" + search + "') " +
                    "or contains(Type, '"+search+ "') or contains(BookId, '" + search+"') ";
            }
            HttpResponseMessage response = await client.GetAsync(BookApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<Book> items = ((JArray)temp.value).Select(x => new Book
            {
                BookId = x["BookId"].ToString(),
                Title = x["Title"].ToString(),
                Type = x["Type"].ToString(),
                PubId = x["PubId"].ToString(),
                Price = (decimal?)x["Price"],
                Advance = (decimal?)x["Advance"],
                Royalty = (decimal?)x["Royalty"],
                YtdSales = (int?)x["YtdSales"],
                Notes = x["Notes"].ToString(),
                PublishedDate = (DateTime?)x["PublishedDate"],
            }).ToList();
            @ViewData["key"] = search;
            return View(items);
        }

        public async Task<IActionResult> Create(Book book)
        {
            HttpResponseMessage responseCategory = await client.GetAsync(PublisherApiUrl);
            string strData = await responseCategory.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<Publisher> publishers = ((JArray)temp.value).Select(x => new Publisher
            {
                PublisherName = x["PublisherName"].ToString(),
                PubId = x["PubId"].ToString(),
            }).ToList();
            ViewBag.Publishers = publishers;
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            string data = JsonSerializer.Serialize(book);
            var response = await client.PostAsync(BookApiUrl, new StringContent(data, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Errors");
        }

        public async Task<IActionResult> Edit(string key)
        {
            //Get List Publisher 
            HttpResponseMessage responseCategory = await client.GetAsync(PublisherApiUrl);
            string strData = await responseCategory.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<Publisher> publishers = ((JArray)temp.value).Select(x => new Publisher
            {
                PublisherName = x["PublisherName"].ToString(),
                PubId = x["PubId"].ToString(),
            }).ToList();
            ViewBag.Publishers = publishers;
            //
            HttpResponseMessage response = await client.GetAsync(BookApiUrl+ $"/{key}");
            string strData1 = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var book = JsonSerializer.Deserialize<Book>(strData1, options);
            return View(book);
        }
        public async Task<IActionResult> EditBook(Book book)
        {

            string data = JsonSerializer.Serialize(book);
            var response = await client.PutAsync(BookApiUrl + $"/{book.BookId}", new StringContent(data, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Errors");
        }

        public async Task<IActionResult> Delete(string key)
        {
            HttpResponseMessage response  = await client.DeleteAsync(BookApiUrl + $"/{key}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index","Errors");
        }


    }
}
