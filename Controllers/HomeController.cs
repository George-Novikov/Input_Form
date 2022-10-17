using Input_Form.Models;
using Input_Form;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;
using Microsoft.AspNetCore.Connections.Features;

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

        [HttpPost]
        public IActionResult PostForm([FromBody] FormTransfer formTransfer)
        {
            if (formTransfer == null)
            {
                formTransfer = new FormTransfer();
            }
            try
            {
                Form form = new Form();
                form.SetFormCreationDateTime();
                form.InitializeDefaultValues();
                form.InitializeDefaultFormulas();
                form.IndicatorA.Value = formTransfer.ValueA;
                form.IndicatorB.Value = formTransfer.ValueB;
                form.IndicatorC.Value = formTransfer.ValueC;
                bool discriminantPositive = form.CalculateValues();

                if (discriminantPositive)
                {
                    return Json(new
                    {
                        valueA = form.IndicatorA.Value,
                        valueB = form.IndicatorB.Value,
                        valueC = form.IndicatorC.Value,
                        discriminant = form.Discriminant.Value,
                        firstResult = form.FirstResult.Value,
                        secondResult = form.SecondResult.Value,
                        success = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        valueA = form.IndicatorA.Value,
                        valueB = form.IndicatorB.Value,
                        valueC = form.IndicatorC.Value,
                        discriminant = form.Discriminant.Value,
                        firstResult = form.FirstResult.Value,
                        secondResult = form.SecondResult.Value,
                        success = false
                    });
                }
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
                form.IndicatorA.Value = formTransfer.ValueA;
                form.IndicatorB.Value = formTransfer.ValueB;
                form.IndicatorC.Value = formTransfer.ValueC;
                form.Discriminant.Value = formTransfer.Discriminant;
                form.FirstResult.Value = formTransfer.FirstResult;
                form.SecondResult.Value = formTransfer.SecondResult;
                form.SetValues();
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