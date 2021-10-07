using CoreShared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreShared.ModelsDto
{
    public class ApiInput
    {
        public string Options { get; set; } = ResultFormatOptions.R.ToString();
        public List<CalculatorInput> Joints { get; set; }
    }
}
