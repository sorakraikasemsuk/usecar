using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class capacityengine
    {
        public int capacityEngineId { get; set; }
        public string capacityEngineName { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
