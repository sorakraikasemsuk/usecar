using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class contactrepairshop
    {
        public int contactRepairShopId { get; set; }
        public int repairShopId { get; set; }
        public string contactName { get; set; }
        public string tel { get; set; }
        public string remark { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public bool isEnable { get; set; }
    }
}
