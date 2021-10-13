using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using CoreShared.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Manager;
using CoreShared.Constants;
using System.Net.Http;
using System.Text;
using System.Net.Mime;

namespace Api.Controllers
{
    [ApiController]
    [Route("/calculate")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorManager _manager;

        public CalculatorController(ICalculatorManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public async Task<JsonResult> Calculate(ApiInput data)
        {
            try
            {
                //Json serializer options (disable case sensitive deserialization)
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

                List<double[,]> listMatrixesAn = new List<double[,]>();
                List<CalculatorResultDto> Joints = new List<CalculatorResultDto>();
                int index = 0;
                data.Options = !String.IsNullOrWhiteSpace(data.Options) ? data.Options : ResultFormatOptions.R.ToString();

                //Generate An matrixes
                foreach (var joint in data.Joints)
                {
                    var matrixAn = _manager.GenerateMatrixAn(joint);
                    listMatrixesAn.Add(matrixAn);
                    if (data.Options.Equals(ResultFormatOptions.F.ToString()))
                    {
                        Joints.Add(new CalculatorResultDto(joint,matrixAn));
                    }
                    index++;
                }

                //Generate Matrix A0
                double[,] MatrixA0 = _manager.GenerateMatrixA0(listMatrixesAn);

                var result = _manager.GetResult(MatrixA0, Joints);

                return new JsonResult(
                        result,options
                    );
            }
            catch (Exception error)
            {
                throw new Exception("An error occurred when receiving data: \"" + error.Message + "\"");
            }
        }
    }
}