using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class car_register
    {
        public int registerId { get; set; }
        public int carId { get; set; }
        public DateTime registerDate { get; set; }
        public string registerNumber { get; set; }
        public int provinceId { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
