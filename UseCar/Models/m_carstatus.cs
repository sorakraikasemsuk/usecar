using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class m_carstatus
    {
        public int carStatusId { get; set; }
        public string statusName { get; set; }
        public bool isEnable { get; set; }
    }
}
