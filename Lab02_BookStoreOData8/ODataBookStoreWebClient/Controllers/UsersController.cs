using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ODataBookStoreWebClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient client = null;
        private string UsersApiUrl = "";

        public UsersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UsersApiUrl = "https://localhost:44355/odata/Users";
        }

        public async Task<IActionResult> Index()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(string email,string pass)
        {
            if(email.Equals("admin@gmail.com") && pass.Equals("1"))
            {
                HttpContext.Session.SetString("Role", "AdminRole");
                return RedirectToAction("Index", "Books");
            }
            return RedirectToAction("Index","Errors");

        }
    }
}
