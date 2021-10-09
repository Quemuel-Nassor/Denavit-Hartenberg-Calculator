namespace CoreShared.ModelsDto
{
    /// <summary>
    /// Class to return result calculated
    /// </summary>
    public class CalculatorResultDto
    {
        public string Axis { get; set; }
        public CalculatorInput InputData { get; set; }

        public string[][] MatrixAn { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CalculatorResultDto()
        {

        }

        /// <summary>
        /// Overloaded consturctor
        /// </summary>
        /// <param name="axis"> current axis </param>
        /// <param name="inputData"> input parameters </param>
        /// <param name="matrixAn"> matrix An to this axis </param>
        public CalculatorResultDto(string axis, CalculatorInput inputData, string[][] matrixAn)
        {
            Axis = axis;
            InputData = inputData;
            MatrixAn = matrixAn;
        }
    }
}