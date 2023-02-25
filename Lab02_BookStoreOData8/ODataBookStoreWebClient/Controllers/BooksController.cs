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

namespace ODataBookStoreWebClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient client = null;
        private string BookApiUrl = "";

        public BooksController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = "https://localhost:44355/odata/Books";
        }

        public async Task<IActionResult> Index()
        {
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
            return View(items);
        }

        public IActionResult Create(Book book)
        {
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
            HttpResponseMessage response = await client.GetAsync(BookApiUrl+ $"/{key}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var book = JsonSerializer.Deserialize<Book>(strData, options);
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
