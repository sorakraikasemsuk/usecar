using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class m_maintenance_status
    {
        public int maintenanceStatusId { get; set; }
        public string statusName { get; set; }
        public bool isEnable { get; set; }
    }
}
