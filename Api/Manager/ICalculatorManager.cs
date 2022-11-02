
using CoreShared.ModelsDto;
using System;
using System.Collections.Generic;

namespace Api.Manager
{
    public interface ICalculatorManager
    {

        /// <summary>
        /// Method to generate An matrix
        /// </summary>
        /// <param name="input">Parameters to generate matrix</param>
        /// <returns> AnMatrix </returns>
        /// <exception cref="Exception"> Generic exception </exception>
        public double[,] GenerateMatrixAn(Joint input);

        /// <summary>
        /// Method to realize product of matrixes
        /// </summary>
        /// <param name="MatrixA"> Matrix of origin of rows </param>
        /// <param name="MatrixB"> Matrix of origin of cols </param>
        /// <returns> Matricial product of A by B </returns>
        /// <exception cref="InvalidOperationException"> If cols A are different from rows B </exception>
        /// <exception cref="Exception"> Generic exception </exception>
        public double[,] MatrixProduct(double[,] MatrixA, double[,] MatrixB);

        /// <summary>
        /// Method to generate A0 matrix
        /// </summary>
        /// <param name="listMatrixes"> List of matrixes An </param>
        /// <returns> A0Matrix </returns>
        /// <exception cref="Exception"> Generic exception </exception>
        public double[,] GenerateMatrixA0(ref List<double[,]> listMatrixes);

        /// <summary>
        /// Method to populate result 
        /// </summary>
        /// <param name="MatrixA0"> Matrix A0 </param>
        /// <param name="input"> calcu </param>
        /// <returns></returns>
        public ApiOutput GetResult(double[,] MatrixA0, List<Result> input);
    }
}