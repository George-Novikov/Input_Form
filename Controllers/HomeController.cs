using Input_Form.Models;
using Input_Form;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Input_Form.Controllers
{
    public class HomeController : Controller
    {
        public Form formInstance = new Form();
        public FormTransfer formBuffer = new FormTransfer();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            formInstance = FormCreator.LoadForm();

            return View(formInstance);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InitialFormLoad()
        {
            Form form = FormCreator.LoadForm();

            return Json(new
            {
                valueA = form.ValueA.Value,
                valueB = form.ValueB.Value,
                valueC = form.ValueC.Value,
                discriminant = form.Discriminant.Value,
                firstResult = form.FirstResult.Value,
                secondResult = form.SecondResult.Value
            });
        }

        [HttpGet]
        public IActionResult GetForm()
        {
            Form form = new Form();
            form.SetFormCreationDateTime();
            form.InitializeDefaultValues();
            form.InitializeDefaultFormulas();
            form.ValueA = formInstance.ValueA;
            form.ValueB = formInstance.ValueB;
            form.ValueC = formInstance.ValueC;
            bool discriminantPositive = form.CalculateValues();

            if (discriminantPositive)
            {
                return Json(new
                {
                    valueA = form.ValueA.Value,
                    valueB = form.ValueB.Value,
                    valueC = form.ValueC.Value,
                    discriminant = form.Discriminant.Value,
                    firstResult = form.FirstResult.Value,
                    secondResult = form.SecondResult.Value
                });
            }
            else
            {
                return Json(new
                {
                    valueA = form.ValueA.Value,
                    valueB = form.ValueB.Value,
                    valueC = form.ValueC.Value,
                    discriminant = form.Discriminant.Value,
                    firstResult = "отриц. дискриминант!",
                    secondResult = "отриц. дискриминант!"
                });
            }
        }

        [HttpPost]
        public IActionResult PostForm([FromBody] FormTransfer formTransfer)
        {
            try
            {
                Form form = new Form();
                form.SetFormCreationDateTime();
                form.InitializeDefaultValues();
                form.InitializeDefaultFormulas();
                form.ValueA.Value = formTransfer.ValueA;
                form.ValueB.Value = formTransfer.ValueB;
                form.ValueC.Value = formTransfer.ValueC;
                form.CalculateValues();

                return Json(new
                {
                    valueA = form.ValueA.Value,
                    valueB = form.ValueB.Value,
                    valueC = form.ValueC.Value,
                    discriminant = form.Discriminant.Value,
                    firstResult = form.FirstResult.Value,
                    secondResult = form.SecondResult.Value
                });
            }
            catch (Exception e)
            {
                return Json(new { status = e.Message + " " + e.TargetSite });
            }
        }

        [HttpPost]
        public IActionResult SaveFormToDB([FromBody] FormTransfer formTransfer)
        {
            try
            {
                Form form = new Form();
                form.SetFormCreationDateTime();
                form.InitializeDefaultValues();
                form.InitializeDefaultFormulas();
                form.CalculateValues();
                form.ValueA.Value = formTransfer.ValueA;
                form.ValueB.Value = formTransfer.ValueB;
                form.ValueC.Value = formTransfer.ValueC;
                form.Discriminant.Value = formTransfer.Discriminant;
                form.FirstResult.Value = formTransfer.FirstResult;
                form.SecondResult.Value = formTransfer.SecondResult;
                FormCreator.SaveForm(form);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { status = e.Message + " " + e.TargetSite });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}