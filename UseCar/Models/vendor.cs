using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class vendor
    {
        public int vendorId { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorTel { get; set; }
        public string vendorNumber { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
