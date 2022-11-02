using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CoreShared.Constants;

namespace CoreShared.ModelsDto
{
    public class Joint
    {
        public Joint()
        {
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="theta"> Theta angle </param>
        /// <param name="distanceD"> Distance D </param>
        /// <param name="distanceA"> Distance A </param>
        /// <param name="alpha"> Alpha Angle </param>
        public Joint(double theta, double distanceD, double distanceA, double alpha)
        {
            Theta = theta;
            DistanceD = distanceD;
            DistanceA = distanceA;
            Alpha = alpha;
        }

        [Required(ErrorMessage = nameof(Theta) + " is required")]
        public double Theta { get; set; }
        public double ThetaRadians => (Math.PI * Theta) / 180;

        [Required(ErrorMessage = nameof(DistanceD) + " is required")]
        public double DistanceD { get; set; }

        [Required(ErrorMessage = nameof(DistanceA) + " is required")]
        public double DistanceA { get; set; }

        [Required(ErrorMessage = nameof(Alpha) + " is required")]
        public double Alpha { get; set; }
        public double AlphaRadians => (Math.PI * Alpha) / 180;

    }
}
