using System;
using System.Collections.Generic;
using System.Text;

namespace CoreShared.ModelsDto
{
    public class JointInput
    {
        public int? Index { get; set; }
        public List<CalculatorInput> Joints { get; set; }
    }
}
