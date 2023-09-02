using Dogtas.MVC.Controllers;
using Dogtas.MVC.Localization.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dogtas.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            return View();
        }
    }
}
