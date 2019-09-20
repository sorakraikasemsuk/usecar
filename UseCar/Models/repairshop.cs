using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class repairshop
    {
        public int repairShopId { get; set; }
        public string repairShopName { get; set; }
        public string repairShopAddress { get; set; }
        public string tel { get; set; }
        public string fax { get; set; }
        public int categoryShopId { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
