using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ODataBookStoreWebClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient client = null;
        private string UsersApiUrl = "";
        private string PublisherApiUrl = "";

        public UsersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UsersApiUrl = "https://localhost:44355/odata/Users";
            PublisherApiUrl = "https://localhost:44355/odata/Publishers";

        }

        public async Task<IActionResult> Index()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Authorization(string email, string pass)
        {
            if (email.Equals("admin@gmail.com") && pass.Equals("1"))
            {
                HttpContext.Session.SetString("Role", "AdminRole");
                return RedirectToAction("Index", "Books");
            }
            HttpResponseMessage response = await client.GetAsync(UsersApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<User> users = ((JArray)temp.value).Select(x => new User
            {
                UserId = x["UserId"].ToString(),
                EmailAddress = x["EmailAddress"].ToString(),
                Password = x["Password"].ToString(),
                RoleId = (bool)x["RoleId"],
            }).ToList();
            foreach (var item in users)
            {
                if (item.EmailAddress.Trim().Equals(email) && item.Password.Trim().Equals(pass))
                {
                    HttpContext.Session.SetString("Role", $"{item.RoleId}");
                    return RedirectToAction("Edit", new { id = item.UserId });
                }
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(string id)
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
            ViewBag.Publishers1 = publishers;
            //

            HttpResponseMessage response = await client.GetAsync(UsersApiUrl + $"/{id}");
            string strData1 = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var user = JsonSerializer.Deserialize<User>(strData1, options);
            return View(user);
 
        }

        public async Task<IActionResult> EditUser(User user)
        {

            string data = JsonSerializer.Serialize(user);
            var response = await client.PutAsync(UsersApiUrl + $"/{user.UserId}", new StringContent(data, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Edit", new { id = user.UserId });
            }
            return RedirectToAction("Index", "Errors");
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
