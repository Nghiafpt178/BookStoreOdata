using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ODataBookStoreWebClient.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly HttpClient client = null;
        private string AuthorApiUrl = "";

        public AuthorsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AuthorApiUrl = "https://localhost:44355/odata/Authors";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(AuthorApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<Author> items = ((JArray)temp.value).Select(x => new Author
            {
                AuthorId = x["AuthorId"].ToString(),
                LastName = x["LastName"].ToString(),
                FirstName = x["FirstName"].ToString(),
                Phone = x["Phone"].ToString(),
                Address = x["Address"].ToString(),
                City = x["City"].ToString(),
                State = x["State"].ToString(),
                Zip = x["Zip"].ToString(),
                EmailAddress = x["EmailAddress"].ToString(),
            }).ToList();
            return View(items);
        }

        public IActionResult Create(Author author)
        {
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(Author author)
        {
            string data = JsonSerializer.Serialize(author);
            var response = await client.PostAsync(AuthorApiUrl, new StringContent(data, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Errors");
        }

        public async Task<IActionResult> Edit(string key)
        {
            HttpResponseMessage response = await client.GetAsync(AuthorApiUrl + $"/{key}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var author = JsonSerializer.Deserialize<Author>(strData, options);
            return View(author);
        }
        public async Task<IActionResult> EditAuthors(Author author)
        {

            string data = JsonSerializer.Serialize(author);
            var response = await client.PutAsync(AuthorApiUrl + $"/{author.AuthorId}", new StringContent(data, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Errors");
        }

        public async Task<IActionResult> Delete(string key)
        {
            HttpResponseMessage response = await client.DeleteAsync(AuthorApiUrl + $"/{key}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Errors");
        }
    }
}
