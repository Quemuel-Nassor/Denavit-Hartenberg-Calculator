
using CoreShared.ModelsDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Core.Manager;
using System.Text.Json;

public class CalculatorManager : ICalculatorManager
{
    /// <summary>
    /// Array for multiplication and obtaining coordinates
    /// </summary>
    private readonly double[,] MultiplierArray = new double[4, 1];

    public CalculatorManager()
    {
        MultiplierArray.SetValue(0,0,0);
        MultiplierArray.SetValue(0,1,0);
        MultiplierArray.SetValue(0,2,0);
        MultiplierArray.SetValue(1,3,0);
    }

    /// <summary>
    /// Method to generate An matrix
    /// </summary>
    /// <param name="input">Parameters to generate matrix</param>
    /// <returns> AnMatrix </returns>
    /// <exception cref="Exception"> Generic exception </exception>
    public double[,] GenerateMatrixAn(CalculatorInput input)
    {
        try
        {
            double[,] MatrixAi = new double[4, 4];

            // row 0
            MatrixAi.SetValue(Math.Cos(input.Theta), 0, 0);
            MatrixAi.SetValue((-Math.Cos(input.Alpha) * Math.Sin(input.Theta)), 0, 1);
            MatrixAi.SetValue(Math.Sin(input.Alpha) * Math.Sin(input.Theta), 0, 2);
            MatrixAi.SetValue(input.DistanceA * Math.Cos(input.Theta), 0, 3);

            // row 1
            MatrixAi.SetValue(Math.Sin(input.Theta), 1, 0);
            MatrixAi.SetValue(Math.Cos(input.Alpha) * Math.Cos(input.Theta), 1, 1);
            MatrixAi.SetValue((-Math.Sin(input.Alpha) * Math.Cos(input.Theta)), 1, 2);
            MatrixAi.SetValue(input.DistanceA * Math.Sin(input.Theta), 1, 3);

            // row 2
            MatrixAi.SetValue(0, 2, 0);
            MatrixAi.SetValue(Math.Sin(input.Alpha), 2, 1);
            MatrixAi.SetValue(Math.Cos(input.Alpha), 2, 2);
            MatrixAi.SetValue(input.DistanceD, 2, 3);

            // row 3
            MatrixAi.SetValue(0, 3, 0);
            MatrixAi.SetValue(0, 3, 1);
            MatrixAi.SetValue(0, 3, 2);
            MatrixAi.SetValue(1, 3, 3);

            return MatrixAi;
        }
        catch (Exception error)
        {
            throw new Exception("an error occurred when generating the AnMatrix");
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
    public double[,] MatrixProduct(double[,] MatrixA, double[,] MatrixB)
    {
        try
        {
            //Get row and col number from A and B
            int rowsA = MatrixA.GetLength(0), rowsB = MatrixB.GetLength(0);
            int colsA = MatrixA.GetLength(1), colsB = MatrixB.GetLength(1);

            if (colsA != rowsB) throw new InvalidOperationException("The number of columns at matrix A need be equals the number of rows at matrix B");

            //Mount result matrix with rowsA and colsB of size
            double[,] result = new double[rowsA, colsB];

            //Parallels the operations of each line
            Parallel.For(0, rowsA, rowA =>
            {
                if(colsB==1) Console.WriteLine("percorrendo linhaA {0} ", rowA);
                for (int colB = 0; colB < colsB; colB++)
                {
                if(colsB==1) Console.WriteLine("percorrendo colB {0} ", colB);
                    for (int i = 0; i < colsA; i++)
                    {
                        if (result.GetValue(rowA, colB) == null) 
                            result.SetValue(0, rowA, colB);

                        var resultValue = Convert.ToDouble(result.GetValue(rowA, colB));
                        var product = (Convert.ToDouble(MatrixA.GetValue(rowA, i)) * Convert.ToDouble(MatrixB.GetValue(i, rowA)));

                        if(colsB==1) Console.WriteLine("mA[{0}][{1}] = {2}",rowA, i,MatrixA.GetValue(rowA, i));
                        if(colsB==1) Console.WriteLine("mB[{0}][{1}] = {2}",i, rowA,MatrixB.GetValue(i, rowA));
                        if(colsB==1) Console.WriteLine("result[{0}][{1}] = {2}",rowA, colB, product);

                        var value = resultValue + product;//Convert.ToDouble(result.GetValue(rowA, rowB)) + (Convert.ToDouble(MatrixA.GetValue(rowA, i)) * Convert.ToDouble(MatrixB.GetValue(i, rowA)));
                        result.SetValue(value, rowA, colB);

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
    public double[,] GenerateMatrixA0(List<double[,]> listMatrixes)
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
    /// <param name="input"> input parameters data list </param>
    /// <returns></returns>
    public ApiOutput GetResult(double[,] MatrixA0, List<CalculatorResultDto> input)
    {
        int rows = MatrixA0.GetLength(0);
        int cols = MatrixA0.GetLength(1);

        ApiOutput result = new ApiOutput()
        {
            Xcoordinate = Convert.ToDouble(MatrixA0.GetValue(0, 3)),
            Ycoordinate = Convert.ToDouble(MatrixA0.GetValue(1, 3)),
            Zcoordinate = Convert.ToDouble(MatrixA0.GetValue(2, 3)),
            Joints = input
        };

        result.MatrixA0 = new List<List<double>>();

        for (int i = 0; i < rows; i++)
        {
            result.MatrixA0.Add(new List<double>());

            for (int j = 0; j < cols; j++)
            {
                result.MatrixA0[i].Add((double)MatrixA0.GetValue(i, j));
            }
        }

        return result;
    }
}