using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class CalculatorController : Controller
    {

        [Route("/calculate")]
        public async Task<IActionResult> Calculate()
        {
            try
            {

                using (HttpClient cli = new HttpClient())
                {
                    var request = await cli.GetAsync("https://localhost:6001/calculate");

                    if (request.IsSuccessStatusCode)
                    {
                        var result = request.Content.ReadAsStringAsync();
                        return new JsonResult(result);
                    }
                    else
                    {
                        return new JsonResult("You request failure with status code: " + request.StatusCode);
                    }
                }

            }
            catch (Exception error)
            {
                throw new Exception("Not was possible connect on API: \"" + error.Message+"\"");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
