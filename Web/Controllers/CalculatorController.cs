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

namespace Web.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string UrlApi;

        public CalculatorController(IConfiguration configuration)
        {
            _configuration = configuration;
            UrlApi = _configuration.GetSection("ApiUrl").Value;
        }

        [HttpPost]
        // [Route("/calculate")]
        public async Task<IActionResult> Calculate()
        {
            try
            {
                //Disable certificate validation
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                //API request data
                using (HttpClient cli = new HttpClient(clientHandler))
                {
                    //API call
                    var jsonApiResponse = await cli.GetStringAsync(UrlApi + "/calculate");

                    //Json serializer options
                    JsonSerializerOptions options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    //Deserialize the answer into a corresponding template list 
                    List<ResultDto> responseDto = JsonSerializer.Deserialize<List<ResultDto>>(jsonApiResponse, options);

                    //Return result
                    // return new JsonResult(responseDto);
                    return PartialView("_calculatorResultPartial", responseDto);
                }

            }
            catch (Exception error)
            {
                throw new Exception("Not was possible connect on API: \"" + error.Message + "\"");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
