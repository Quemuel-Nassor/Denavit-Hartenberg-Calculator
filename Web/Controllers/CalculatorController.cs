using Models.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpPost]
        public IActionResult Calculate(ApiInput input)
        {
            return ViewComponent("GetCalculatorResult", input);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
