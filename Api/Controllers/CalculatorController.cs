using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using CoreShared.ModelsDto;

namespace Api.Controllers
{
    public class CalculatorController : ControllerBase
    {
        public CalculatorController()
        {

        }

        [HttpGet]
        [Route("/calculate")]
        public JsonResult GetResult()
        {
            var result = JsonSerializer.Serialize(new ResultDto()
            {
                Xcoordinate = Convert.ToDecimal(5.1),
                Ycoordinate = Convert.ToDecimal(7.22),
                Zcoordinate = Convert.ToDecimal(0.5232)

            });
            return new JsonResult(
                    new ResultDto()
            {
                Xcoordinate = Convert.ToDecimal(5.1),
                Ycoordinate = Convert.ToDecimal(7.22),
                Zcoordinate = Convert.ToDecimal(0.5232)

            }
                );
        }
    }
}