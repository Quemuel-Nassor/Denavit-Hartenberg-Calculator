using System.Collections.Generic;

namespace CoreShared.ModelsDto
{
    /// <summary>
    /// Class to return result calculated
    /// </summary>
    public class CalculatorResultDto
    {
        public CalculatorInput InputInfo { get; set; }

        public List<List<double>> MatrixAn { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CalculatorResultDto()
        {

        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="inputData"> input data </param>
        /// <param name="matrixAn"> dynamic An matrix </param>
        public CalculatorResultDto(CalculatorInput inputData, double[,] matrixAn)
        {
            int rows = matrixAn.GetLength(0);
            int cols = matrixAn.GetLength(1);
            MatrixAn = new List<List<double>>();

            for (int i = 0; i < rows; i++)
            {
                MatrixAn.Add(new List<double>());

                for (int j = 0; j < cols; j++)
                {
                    MatrixAn[i].Add((double)matrixAn.GetValue(i, j));
                }
            }

            InputInfo = inputData;
        }
    }
}