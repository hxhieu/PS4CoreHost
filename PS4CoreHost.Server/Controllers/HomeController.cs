using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PS4CoreHost.Server.Models;
using PS4CoreHost.Utils;
using System.Diagnostics;
using System.IO;

namespace PS4CoreHost.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPayloadSender _payload;
        private readonly IHostingEnvironment _env;

        public HomeController(IPayloadSender payload, IHostingEnvironment env)
        {
            _payload = payload;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            var payload = System.IO.File.ReadAllBytes(Path.Combine(_env.ContentRootPath, "PS4HEN.bin"));
            _payload.Send("192.168.0.230", payload);
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
