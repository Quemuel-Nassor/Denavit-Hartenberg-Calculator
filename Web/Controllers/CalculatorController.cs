using CoreShared.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Models;
using Microsoft.Extensions.Configuration;
using CoreShared.Constants;

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
