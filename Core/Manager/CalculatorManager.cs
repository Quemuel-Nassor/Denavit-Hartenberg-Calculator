
using CoreShared.ModelsDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class CalculatorManager
{
    /// <summary>
    /// Array for multiplication and obtaining coordinates
    /// </summary>
    static readonly double[,] MultiplierArray = { { 0 }, { 0 }, { 0 }, { 1 } };

    public CalculatorManager()
    {
    }

    /// <summary>
    /// Method to generate An matrix
    /// </summary>
    /// <param name="input">Parameters to generate matrix</param>
    /// <returns> AnMatrix </returns>
    /// <exception cref="Exception"> Generic exception </exception>
    static double[,] GenerateMatrixAn(CalculatorInput input)
    {
        try
        {
            double[,] MatrixAi = new double[4, 4];
            MatrixAi.SetValue(new double[4] { Math.Cos(input.Theta), (-Math.Cos(input.Alpha) * Math.Sin(input.Theta)), Math.Sin(input.Alpha) * Math.Sin(input.Theta), input.DistanceA * Math.Cos(input.Theta) }, 0);
            MatrixAi.SetValue(new double[4] { Math.Sin(input.Theta), Math.Cos(input.Alpha) * Math.Cos(input.Theta), (-Math.Sin(input.Alpha) * Math.Cos(input.Theta)), input.DistanceA * Math.Sin(input.Theta) }, 1);
            MatrixAi.SetValue(new double[4] { 0, Math.Sin(input.Alpha), Math.Cos(input.Alpha), input.DistanceD }, 2);
            MatrixAi.SetValue(new double[4] { 0, 0, 0, 1 }, 3);
            //{
            //    new double[4]{ Math.Cos(input.Theta), (-Math.Cos(input.Alpha) * Math.Sin(input.Theta)), Math.Sin(input.Alpha) * Math.Sin(input.Theta)   , input.DistanceA * Math.Cos(input.Theta) },
            //    new double[4]{ Math.Sin(input.Theta), Math.Cos(input.Alpha) * Math.Cos(input.Theta)   , (-Math.Sin(input.Alpha) * Math.Cos(input.Theta)), input.DistanceA * Math.Sin(input.Theta) },
            //    new double[4]{ 0                    , Math.Sin(input.Alpha)                           , Math.Cos(input.Alpha)                           , input.DistanceD                         },
            //    new double[4]{ 0                    , 0                                               , 0                                               , 1                                       },
            //};

            return MatrixAi;
        }
        catch (Exception error)
        {
            throw new Exception(String.Format("an error occurred when generating the {0} axis matrix", input.Axis));
        }
    }

    /// <summary>
    /// Method to realize product of matrixes
    /// </summary>
    /// <param name="MatrixA"> Matrix of origin of rows </param>
    /// <param name="MatrixB"> Matrix of origin of cols </param>
    /// <returns> Matricial product of A by B </returns>
    /// <exception cref="InvalidOperationException"> If cols A are different from rows B </exception>
    /// <exception cref="Exception"> Generic exception </exception>
    static double[,] MatrixProduct(double[,] MatrixA, double[,] MatrixB)
    {
        try
        {
            //Get row and col number from A and B
            int rowsA = MatrixA.Rank, rowsB = MatrixB.Rank;
            int colsA = MatrixA.GetLength(1), colsB = MatrixB.GetLength(1);

            if (colsA != rowsB) throw new InvalidOperationException("The number of columns at matrix A need be equals the number of rows at matrix B");

            //Mount result matrix with rowsA and colsB of size
            double[,] result = new double[rowsA, colsB];

            //Parallel.For(0, colsA, index =>
            //    result[index] = new double[colsB]
            //);

            //Parallels the operations of each line
            Parallel.For(0, rowsA, rowA =>
            {
                for (int colB = 0; colB < colsB; colB++)
                {
                    for (int i = 0; i < colsB; i++)
                    {
                        //result[rowA][colB] += MatrixA.GetValue(rowA,colB) * MatrixB.GetValue(colB,rowA);
                        var value = Convert.ToDouble(MatrixA.GetValue(rowA, colB)) * Convert.ToDouble(MatrixB.GetValue(colB, rowA));
                        result.SetValue(Convert.ToDouble(result.GetValue(rowA, colB)) + value, rowA, colsB);

                    }
                }
            }
            );

            return result;
        }
        catch (Exception error)
        {
            throw new Exception("An error occurred when multiplying matrix A and matrix B");
        }
    }

    /// <summary>
    /// Method to generate A0 matrix
    /// </summary>
    /// <param name="listMatrixes"> List of matrixes An </param>
    /// <returns> A0Matrix </returns>
    /// <exception cref="Exception"> Generic exception </exception>
    static double[,] GenerateMatrixA0(List<double[,]> listMatrixes)
    {
        try
        {
            List<double[,]> productList = new List<double[,]>();

            for (int i = 1; i < listMatrixes.Count; i += 2)
            {
                //Validate last index
                if (i < listMatrixes.Count)
                {
                    //If list count is odd and is last element
                    if (i == listMatrixes.Count - 1 && listMatrixes.Count % 2 != 0)
                    {
                        productList.Add(listMatrixes[i]);
                    }
                    else
                    {
                        var product = MatrixProduct(MatrixA: listMatrixes[i - 1], MatrixB: listMatrixes[i]);
                        productList.Add(product);
                    }
                }
            }

            //Recursion to reduce list to a single element
            if (productList.Count > 1) GenerateMatrixA0(productList);

            return productList.FirstOrDefault();
        }
        catch (Exception error)
        {
            throw new Exception("An error occurred when multiplying list of matrixes");
        }
    }

    /// <summary>
    /// Method to populate result 
    /// </summary>
    /// <param name="MatrixA0"> Matrix A0 </param>
    /// <param name="input"> calcu </param>
    /// <returns></returns>
    static CalculatorResultDto GetResult(double[,] MatrixA0, CalculatorInput input)
    {

        var coordinates = MatrixProduct(MatrixA0, MultiplierArray);

        CalculatorResultDto result = new CalculatorResultDto(
            x: Convert.ToDouble(coordinates.GetValue(0, 0)),
            y: Convert.ToDouble(coordinates.GetValue(1, 0)),
            z: Convert.ToDouble(coordinates.GetValue(2, 0)),
            matrixAn:
                new string[4][]{
                        new string[]{ String.Format("Cos({0})",input.Theta), String.Format("-Cos({0}) * Sin({1})",input.Alpha,input.Theta), String.Format("Sin({0}) * Sin({1})",input.Alpha,input.Theta), String.Format("{0} * Cos({1})",input.DistanceA,input.Theta) },
                        new string[]{ String.Format("Sin({0})",input.Theta), String.Format("Cos({0}) * Cos({1})",input.Alpha,input.Theta), String.Format("-Sin({0}) * Cos({1})",input.Alpha,input.Theta), String.Format("{0} * Sin({1})",input.DistanceA,input.Theta) },
                        new string[]{ "0", String.Format("Sin({0})",input.Alpha), String.Format("Cos({0})",input.Alpha), input.DistanceD.ToString() },
                        new string[]{"0", "0", "0", "1" },
                    }
                );

        return result;
    }
}