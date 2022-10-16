using Input_Form.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Input_Form.Controllers
{
    public class HomeController : Controller
    {
        public FormTransfer formBuffer;
        public string stringBuffer;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Form form = FormCreator.LoadForm();

            return View(form);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetForm()
        {
            Form form = new Form();
            form.SetFormCreationDateTime();
            form.InitializeDefaultValues();
            form.ValueA.Value = 135;
            form.ValueB.Value = 120;
            form.ValueC.Value = 76;
            form.InitializeDefaultFormulas();

            //TO DO: load updated Form from buffer and send as JSON object

            return View(form);
        }
        
        [HttpPost]
        public IActionResult PostForm([FromBody]FormTransfer formTransfer)
        {
            //FormTransfer formTransfer = JsonSerializer.Deserialize<FormTransfer>(transfer);
            
            try
            {
                Form form = new Form();
                form.SetFormCreationDateTime();
                form.InitializeDefaultValues();
                form.InitializeDefaultFormulas();
                form.ValueA.Value = Convert.ToDouble(formTransfer.ValueA);
                form.ValueB.Value = Convert.ToDouble(formTransfer.ValueB);
                form.ValueC.Value = Convert.ToDouble(formTransfer.ValueC);
                form.Discriminant.Value = Convert.ToDouble(formTransfer.Discriminant);
                form.FirstResult.Value = Convert.ToDouble(formTransfer.FirstResult);
                form.SecondResult.Value = Convert.ToDouble(formTransfer.SecondResult);
                FormCreator.SaveForm(form);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { status = e.Message });
            }

            
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