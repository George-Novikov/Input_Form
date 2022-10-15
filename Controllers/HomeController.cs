using Input_Form.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

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
            Form form = Form.LoadForm();

            return View(form);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetForm()
        {
            FormTransfer formTransfer = new FormTransfer();

            //TO DO: load Form from db and send as JSON object

            return new JsonResult(Ok(formTransfer));
        }

        [HttpPost]
        public JsonResult PostForm(FormTransfer formTransfer)
        {
            return Json(new { success = true });
        }

        [HttpPost]
        public void SaveFormToDB()
        {
            //TO DO: recieve Form and save to db
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}