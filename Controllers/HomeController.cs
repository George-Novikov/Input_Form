using Input_Form.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

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

            //TO DO: load Form from db and send as JSON object

            return new JsonResult(Ok());
        }
        
        [HttpPost]
        public JsonResult PostForm(FormTransfer formTransfer)
        {
            //FormTransfer formTransfer = JsonSerializer.Deserialize<FormTransfer>(transfer);
            Form form = new Form();
            form.ValueA.Value = Convert.ToDouble(formTransfer.ValueA);
            form.ValueB.Value = Convert.ToDouble(formTransfer.ValueB);
            form.ValueC.Value = Convert.ToDouble(formTransfer.ValueC);
            form.Discriminant.Value = Convert.ToDouble(formTransfer.Discriminant);
            form.FirstResult.Value = Convert.ToDouble(formTransfer.FirstResult);
            form.SecondResult.Value = Convert.ToDouble(formTransfer.SecondResult);
            FormCreator.SaveForm(form);

            return Json( new { success = true } );
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