using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class drivesystem
    {
        public int driveSystemId { get; set; }
        public string driveSystemName { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
