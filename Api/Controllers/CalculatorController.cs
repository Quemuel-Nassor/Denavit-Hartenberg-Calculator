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
                List<double[,]> listMatrixesAn = new List<double[,]>();
                List<CalculatorResultDto> Joints = new List<CalculatorResultDto>();
                int index = 0;
                data.Options = !String.IsNullOrWhiteSpace(data.Options) ? data.Options : ResultFormatOptions.R.ToString();

                //Generate An matrixes
                foreach (var joint in data.Joints)
                {
                    listMatrixesAn.Add(_manager.GenerateMatrixAn(joint));
                    if (data.Options.Equals(ResultFormatOptions.F.ToString()))
                    {
                        Joints.Add(new CalculatorResultDto(
                            "j" + index.ToString(),
                            joint,
                            new string[4][]{
                            new string[]{ String.Format("Cos({0})",joint.Theta), String.Format("-Cos({0}) * Sin({1})",joint.Alpha,joint.Theta), String.Format("Sin({0}) * Sin({1})",joint.Alpha,joint.Theta), String.Format("{0} * Cos({1})",joint.DistanceA,joint.Theta) },
                            new string[]{ String.Format("Sin({0})",joint.Theta), String.Format("Cos({0}) * Cos({1})",joint.Alpha,joint.Theta), String.Format("-Sin({0}) * Cos({1})",joint.Alpha,joint.Theta), String.Format("{0} * Sin({1})",joint.DistanceA,joint.Theta) },
                            new string[]{ "0", String.Format("Sin({0})",joint.Alpha), String.Format("Cos({0})",joint.Alpha), joint.DistanceD.ToString() },
                            new string[]{ "0", "0", "0", "1" },
                            }
                            ));
                    }
                    index++;
                }

                //Generate Matrix A0
                double[,] MatrixA0 = _manager.GenerateMatrixA0(listMatrixesAn);

                var result = _manager.GetResult(MatrixA0, Joints);

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