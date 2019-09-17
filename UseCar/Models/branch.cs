using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class branch
    {
        public int branchId { get; set; }
        public string branchName { get; set; }
        public string branchAddress { get; set; }
        public string tel { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
