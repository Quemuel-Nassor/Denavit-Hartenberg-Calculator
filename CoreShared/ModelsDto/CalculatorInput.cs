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
        /// <param name="options"> Result Options </param>
        public CalculatorInput(decimal theta, decimal distanceD, decimal distanceA, decimal alpha, string options)
        {
            Theta = theta;
            DistanceD = distanceD;
            DistanceA = distanceA;
            Alpha = alpha;
            if (!String.IsNullOrWhiteSpace(options)) Options = options;
        }

        [Required(ErrorMessage = nameof(Theta) + " is required")]
        public decimal Theta { get; set; }

        [Required(ErrorMessage = nameof(DistanceD) + " is required")]
        public decimal DistanceD { get; set; }

        [Required(ErrorMessage = nameof(DistanceA) + " is required")]
        public decimal DistanceA { get; set; }

        [Required(ErrorMessage = nameof(Alpha) + " is required")]
        public decimal Alpha { get; set; }
        public string Options { get; set; } = ResultFormatOptions.R.ToString();
    }
}
