namespace CoreShared.ModelsDto
{
    /// <summary>
    /// Class to return result calculated
    /// </summary>
    public class CalculatorResultDto
    {
        public string Axis { get; set; }
        public double Xcoordinate { get; set; }
        public double Ycoordinate { get; set; }
        public double Zcoordinate { get; set; }

        public string[][] MatrixAn { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CalculatorResultDto()
        {

        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="x"> X coordinate </param>
        /// <param name="y"> Y coordinate </param>
        /// <param name="z"> Z coordinate </param>
        /// <param name="matrixAn"> MatrixAn of calculated results </param>
        public CalculatorResultDto(double x, double y, double z, string[][] matrixAn)
        {
            this.Xcoordinate = x;
            this.Ycoordinate = y;
            this.Zcoordinate = z;
            this.MatrixAn = matrixAn;
        }

    }
}