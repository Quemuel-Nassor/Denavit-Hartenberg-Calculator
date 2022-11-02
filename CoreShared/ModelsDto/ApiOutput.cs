using CoreShared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreShared.ModelsDto
{
    public class ApiOutput
    {
        public double Xcoordinate { get; set; }
        public double Ycoordinate { get; set; }
        public double Zcoordinate { get; set; }
        public List<List<double>> MatrixA0 { get; set; }
        public List<Result> JointsInfo { get; set; }
    }
}
