using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class car_owner
    {
        public int ownerId { get; set; }
        public int carId { get; set; }
        public int registerId { get; set; }
        public int order { get; set; }
        public DateTime ownerDate { get; set; }
        public string ownerName { get; set; }
        public string ownerAddress { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
