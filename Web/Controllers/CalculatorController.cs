using CoreShared.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Models;
using System.Text.Json.Serialization;

namespace Web.Controllers
{
    public class CalculatorController : Controller
    {

        [Route("/calculate")]
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
                    var request = await cli.GetAsync("https://localhost:6001/calculate");

                    //Response verification
                    if (request.IsSuccessStatusCode)
                    {
                        //Read string request content
                        var result = await request.Content.ReadAsStringAsync();

                        JsonSerializerOptions opt = new JsonSerializerOptions() { AllowTrailingCommas = true };

                        //Deserialize the answer into a corresponding template list 
                        List<ResultDto> response = JsonSerializer.Deserialize<List<ResultDto>>(result, opt);
                        List<dynamic> test = JsonSerializer.Deserialize<List<dynamic>>(result, opt);

                        //return json list
                        return new JsonResult(test);
                    }
                    else
                    {
                        return new JsonResult("You request failure with status code: " + request.StatusCode);
                    }
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
