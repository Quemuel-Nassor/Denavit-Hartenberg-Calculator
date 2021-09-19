using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            return new JsonResult(
                    new
                    {
                        Name = "test",
                        Value = "ReturnOk"
                    }
                );
        }
    }
}