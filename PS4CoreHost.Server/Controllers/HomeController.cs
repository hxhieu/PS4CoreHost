using Microsoft.AspNetCore.Mvc;
using PS4CoreHost.Server.Models;
using System.Diagnostics;

namespace PS4CoreHost.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
