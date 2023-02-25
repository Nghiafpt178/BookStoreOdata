using Microsoft.AspNetCore.Mvc;

namespace ODataBookStoreWebClient.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
