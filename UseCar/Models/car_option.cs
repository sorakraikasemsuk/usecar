using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class car_option
    {
        public int carOptionId { get; set; }
        public int carId { get; set; }
        public int optionId { get; set; }
    }
}
