using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreShared.ModelsDto
{
    public class CalculatorInput
    {
        [Required(ErrorMessage = nameof(Theta) + " is required")]
        public decimal Theta { get; set; }
        [Required(ErrorMessage = nameof(DistanceD) + " is required")]
        public decimal DistanceD { get; set; }
        [Required(ErrorMessage = nameof(DistanceA) + " is required")]
        public decimal DistanceA { get; set; }
        [Required(ErrorMessage = nameof(Alpha) + " is required")]
        public decimal Alpha { get; set; }
    }
}
