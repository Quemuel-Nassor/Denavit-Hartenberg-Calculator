using CoreShared.Constants;
using CoreShared.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string UrlApi;

        public HomeController(
            IConfiguration configuration,
            ILogger<HomeController> logger,
            IHostEnvironment env)
        {
            _env = env;
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
            ViewData["Title"] = "Api";
            ViewData["TitlePage"] = "Api Documentation";

            ViewData["ApiEndpoint"] = UrlApi;

            var exampleInputData = new ApiInput()
            {
                Options = ResultFormatOptions.F.ToString(),
                Joints = new List<CalculatorInput>(){
                    new CalculatorInput(theta: 90,distanceD: 4,distanceA: 2,alpha: 0),
                    new CalculatorInput(theta: 180,distanceD: 0,distanceA: 1,alpha: 180),
                    new CalculatorInput(theta: 0,distanceD: 2,distanceA: 0,alpha: 0),
                    new CalculatorInput(theta: 90,distanceD: 0,distanceA: 0,alpha: 0)
                }
            };

            //Json serializer options (disable case sensitive deserialization)
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            //Serialize input model
            var jsonInputString = JsonSerializer.Serialize(exampleInputData, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
            var content = new StringContent(jsonInputString, Encoding.UTF8, MediaTypeNames.Application.Json);

            string response;

            //Disable ssl certificate validation (for linux environtments)
            HttpClientHandler clientHandler = new HttpClientHandler();
            if (_env.IsDevelopment() && Environment.OSVersion.Platform == PlatformID.Unix)
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            }

            //API request call (with resource auto dispose)
            using (HttpClient cli = new HttpClient(clientHandler))
            {
                //Post request on API
                var jsonApiResponse = cli.PostAsync(UrlApi, content).Result;

                //Check if response return SUCCESS
                jsonApiResponse.EnsureSuccessStatusCode();

                //Deserialize response
                response = jsonApiResponse.Content.ReadAsStringAsync().Result;
            }
            var responseDeserialized = JsonSerializer.Deserialize<ApiOutput>(response, options);

            ViewData["CurlExample"] = "curl " + UrlApi + " -X POST -v -H \"Content-type: application/json\" -d \n\n'" + jsonInputString + "'";
            ViewData["ExampleResponse"] = JsonSerializer.Serialize(responseDeserialized, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, WriteIndented = true });

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
