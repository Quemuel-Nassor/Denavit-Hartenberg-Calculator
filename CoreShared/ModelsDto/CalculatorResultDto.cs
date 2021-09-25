namespace CoreShared.ModelsDto
{
    /// <summary>
    /// Class to return result calculated
    /// </summary>
    public class CalculatorResultDto
    {
        public string Joint { get; set; }
        public decimal Xcoordinate { get; set; }
        public decimal Ycoordinate { get; set; }
        public decimal Zcoordinate { get; set; }

        public decimal[][] ResultMatrix { get; set; }

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
        /// <param name="resultMatrix"> Matrix of calculated results </param>
        public CalculatorResultDto(decimal x, decimal y, decimal z, decimal[][] resultMatrix)
        {
            this.Xcoordinate = x;
            this.Ycoordinate = y;
            this.Zcoordinate = z;
            this.ResultMatrix = resultMatrix;
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="joint"> Target joint of coordinates </param>
        /// <param name="x"> X coordinate </param>
        /// <param name="y"> Y coordinate </param>
        /// <param name="z"> Z coordinate </param>
        /// <param name="resultMatrix"> Matrix of calculated results </param>
        public CalculatorResultDto(string joint, decimal xcoordinate, decimal ycoordinate, decimal zcoordinate, decimal[][] resultMatrix)
        {
            Joint = joint;
            Xcoordinate = xcoordinate;
            Ycoordinate = ycoordinate;
            Zcoordinate = zcoordinate;
            ResultMatrix = resultMatrix;
        }
    }
}