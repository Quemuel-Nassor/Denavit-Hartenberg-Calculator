namespace CoreShared.ModelsDto
{
    /// <summary>
    /// Class to return result calculated
    /// </summary>
    public class CalculatorResultDto
    {
        public decimal Xcoordinate { get; set; }
        public decimal Ycoordinate { get; set; }
        public decimal Zcoordinate { get; set; }

        public string[][] ResultMatrix { get; set; }

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
        public CalculatorResultDto(decimal x, decimal y, decimal z, string[][] resultMatrix)
        {
            this.Xcoordinate = x;
            this.Ycoordinate = y;
            this.Zcoordinate = z;
            this.ResultMatrix = resultMatrix;
        }

    }
}