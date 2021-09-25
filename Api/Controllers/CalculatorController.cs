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
    public class CalculatorController : ControllerBase
    {
        public CalculatorController()
        {

        }

        [HttpGet]
        [Route("/calculate")]
        public async Task<JsonResult> Calculate()
        {
            var result = new List<CalculatorResultDto>(){
                new CalculatorResultDto()
                {
                    Joint = "j1",
                    Xcoordinate = Convert.ToDecimal(5.1),
                    Ycoordinate = Convert.ToDecimal(7.22),
                    Zcoordinate = Convert.ToDecimal(0.5232)
                },
                new CalculatorResultDto()
                {
                    Joint = "j2",
                    Xcoordinate = Convert.ToDecimal(7.7),
                    Ycoordinate = Convert.ToDecimal(9.2),
                    Zcoordinate = Convert.ToDecimal(28991.321)
                }
            };

            return new JsonResult(
                    result
                );
        }
    }
}