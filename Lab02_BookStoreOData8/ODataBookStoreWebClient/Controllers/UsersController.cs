using Microsoft.AspNetCore.Mvc;

namespace ODataBookStoreWebClient.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
