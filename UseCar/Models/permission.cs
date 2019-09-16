using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class permission
    {
        public int permissionId { get; set; }
        public int departmentId { get; set; }
        public int menuPermissionId { get; set; }
    }
}
