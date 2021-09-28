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
        [HttpGet("/{data}")]
        public async Task<JsonResult> Calculate([FromQuery] CalculatorInput data)
        {
            try
            {
                var result = new CalculatorResultDto()
                {
                    Xcoordinate = Convert.ToDecimal(5.1),
                    Ycoordinate = Convert.ToDecimal(7.22),
                    Zcoordinate = Convert.ToDecimal(0.5232)
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