using CoreShared.Constants;
using CoreShared.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string UrlApi;

        public HomeController(
            IConfiguration configuration,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            UrlApi = _configuration.GetSection("ApiUrl").Value + "/calculate";
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        [Route("/api")]
        public IActionResult Api()
        {
            ViewData["Title"] = "Api Documentation";

            ViewData["ApiEndpoint"] = UrlApi;

            return View();
        }

        [Route("/calculator")]
        public IActionResult Calculator()
        {
            ViewData["Title"] = "Calculator";

            var model = new ApiInput() { Joints = new List<CalculatorInput>() { new CalculatorInput() } };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
