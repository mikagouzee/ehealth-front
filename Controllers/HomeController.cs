using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PatientsMvc.Models;
using PatientsMvc.Models.Requests;
using PatientsMvc.Service;

namespace PatientsMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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


        public IActionResult Login([Bind("Name, Password")] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var client = new ApiClient("http://localhost:8080");
            try
            {
                client.Login(request);
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine("oops: ", ex.Message);
                return Error();
            }
        }
    }
}
