using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebSalesMVC.Models.ViewModels;

namespace WebSalesMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "WebSalesMVC app make in CSHARP and .Net Core with MVC";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Autor"] = "Thales Lima";
            ViewData["Email"] = "lthales53@gmail.com";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
