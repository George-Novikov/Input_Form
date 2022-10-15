using Input_Form.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Input_Form.Controllers
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
            Form form = Indicator.InitializeForm();

            return View(form);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public void GetFormValues()
        {

        }
        [HttpPost]
        public void PostFormValues(string valueA, string valueB, string valueC, string discriminant, string firstResult, string secondResult)
        {

        }

        public void SaveFormToDB()
        {

        }

        public void RestoreLastFormCondition()
        {

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}