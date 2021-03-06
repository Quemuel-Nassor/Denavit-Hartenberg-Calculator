using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CoreShared.Constants;

namespace CoreShared.ModelsDto
{
    public class CalculatorInput
    {
        public CalculatorInput()
        {
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="theta"> Theta angle </param>
        /// <param name="distanceD"> Distance D </param>
        /// <param name="distanceA"> Distance A </param>
        /// <param name="alpha"> Alpha Angle </param>
        public CalculatorInput(double theta, double distanceD, double distanceA, double alpha)
        {
            Theta = theta;
            DistanceD = distanceD;
            DistanceA = distanceA;
            Alpha = alpha;
        }

        [Required(ErrorMessage = nameof(Theta) + " is required")]
        public double Theta { get; set; }

        [Required(ErrorMessage = nameof(DistanceD) + " is required")]
        public double DistanceD { get; set; }

        [Required(ErrorMessage = nameof(DistanceA) + " is required")]
        public double DistanceA { get; set; }

        [Required(ErrorMessage = nameof(Alpha) + " is required")]
        public double Alpha { get; set; }

    }
}
