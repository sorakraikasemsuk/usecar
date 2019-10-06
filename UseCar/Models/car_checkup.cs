using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class car_checkup
    {
        public int carCheckupId { get; set; }
        public int carId { get; set; }
        public DateTime checkupDate { get; set; }
        public int checkupBy { get; set; }
        public string remark { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
