using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class car_maintenance_detail
    {
        public int maintenanceDetailId { get; set; }
        public int maintenanceId { get; set; }
        public int itemNo { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
