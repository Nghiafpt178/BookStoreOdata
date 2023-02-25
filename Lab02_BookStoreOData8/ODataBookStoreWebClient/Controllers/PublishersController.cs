using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ODataBookStoreWebClient.Controllers
{
    public class PublishersController : Controller
    {
        private readonly HttpClient client = null;
        private string PublisherApiUrl = "";

        public PublishersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            PublisherApiUrl = "https://localhost:44355/odata/Publishers";
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(PublisherApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<Publisher> items = ((JArray)temp.value).Select(x => new Publisher
            {
                PubId = x["PubId"].ToString(),
                PublisherName = x["PublisherName"].ToString(),
                City = x["City"].ToString(),
                State = x["State"].ToString(),              
                Country = x["Country"].ToString(),
            }).ToList();
            return View(items);
        }
        public IActionResult Create(Publisher publisher)
        {
            return View(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublisher(Publisher publisher)
        {
            string data = JsonSerializer.Serialize(publisher);
            var response = await client.PostAsync(PublisherApiUrl, new StringContent(data, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Errors");
        }

        public async Task<IActionResult> Edit(string key)
        {
            HttpResponseMessage response = await client.GetAsync(PublisherApiUrl + $"/{key}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var publisher = JsonSerializer.Deserialize<Publisher>(strData, options);
            return View(publisher);
        }
        public async Task<IActionResult> EditPublisher(Publisher publisher)
        {

            string data = JsonSerializer.Serialize(publisher);
            var response = await client.PutAsync(PublisherApiUrl + $"/{publisher.PubId}", new StringContent(data, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Errors");
        }

        public async Task<IActionResult> Delete(string key)
        {
            HttpResponseMessage response = await client.DeleteAsync(PublisherApiUrl + $"/{key}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Errors");
        }

    }
}
