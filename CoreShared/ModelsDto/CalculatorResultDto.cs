namespace CoreShared.ModelsDto
{
    /// <summary>
    /// Class to return result calculated
    /// </summary>
    public class CalculatorResultDto
    {
        public CalculatorInput InputData { get; set; }

        public string[][] MatrixAn { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CalculatorResultDto()
        {

        }

    }
}