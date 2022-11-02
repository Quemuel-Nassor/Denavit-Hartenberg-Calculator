using Models.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ModelsDto
{
    public class ApiInput
    {
        public string Options { get; set; } = ResultFormatOptions.S.ToString();
        public List<Joint> Joints { get; set; }
    }
}
