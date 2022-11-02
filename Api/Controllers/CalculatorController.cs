using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Models.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Constants;
using System.Text.RegularExpressions;
using Api.Manager;

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
        public async Task<ApiOutput> Calculate(ApiInput data)
        {
            try
            {
                List<double[,]> listMatrixesAn = new List<double[,]>();
                List<Result> Joints = new List<Result>();
                int index = 0;

                data.Options = Regex.Replace(data.Options, @"[^(s|S|f|F)]*","").ToUpper();
                data.Options = String.IsNullOrWhiteSpace(data.Options) ? ResultFormatOptions.S.ToString() : Enum.GetNames(typeof(ResultFormatOptions)).Contains(data.Options) ? data.Options : ResultFormatOptions.S.ToString();

                //Generate An matrixes
                foreach (var joint in data.Joints)
                {
                    var matrixAn = _manager.GenerateMatrixAn(joint);
                    listMatrixesAn.Add(matrixAn);
                    if (data.Options.Equals(ResultFormatOptions.F.ToString()))
                    {
                        Joints.Add(new Result(joint, matrixAn));
                    }
                    index++;
                }

                //Generate Matrix A0
                double[,] MatrixA0 = _manager.GenerateMatrixA0(ref listMatrixesAn);

                var result = _manager.GetResult(MatrixA0, Joints);

                return await Task.FromResult(result);
            }
            catch (Exception error)
            {
                throw new Exception("An error occurred when receiving data: \"" + error.Message + "\"");
            }
        }
    }
}