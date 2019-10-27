using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class car_history
    {
        public int historyId { get; set; }
        public int carId { get; set; }
        public int menuId { get; set; }
        public int statusId { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public bool isEnable { get; set; }
    }
}
