using Microsoft.AspNetCore.Mvc;

namespace Dogtas.MVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
