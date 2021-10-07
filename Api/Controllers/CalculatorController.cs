using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using CoreShared.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{

    [ApiController]
    [Route("/calculate")]
    public class CalculatorController : ControllerBase
    {
        [HttpPost]
        public async Task<JsonResult> Calculate(ApiInput data)
        {
            try
            {
                var result = new List<CalculatorResultDto>(){
                    new CalculatorResultDto()
                    {
                        Axis = "j1",
                        Xcoordinate = 5.1,
                        Ycoordinate = 7.22,
                        Zcoordinate = 0.5232
                    },
                    new CalculatorResultDto()
                    {
                        Axis = "j2",
                        Xcoordinate = 5.1,
                        Ycoordinate = 7.22,
                        Zcoordinate = 0.5232
                    }
                };

                return new JsonResult(
                        result
                    );
            }
            catch (Exception error)
            {
                throw new Exception("An error occurred when receiving data: \"" + error.Message + "\"");
            }
        }
    }
}