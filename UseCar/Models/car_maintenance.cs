using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class car_maintenance
    {
        public int maintenanceId { get; set; }
        public string code { get; set; }
        public int carId { get; set; }
        public int repairShopId { get; set; }
        public DateTime sendDate { get; set; }
        public DateTime receiveDate { get; set; }
        public string remark { get; set; }
        public int maintenanceStatusId { get; set; }
        public int sendById { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
