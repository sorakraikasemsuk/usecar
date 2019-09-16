using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class m_menupermission
    {
        public int menuPermissionId { get; set; }
        public int menuId { get; set; }
        public string permission { get; set; }
        public string permissionName { get; set; }
        public int ord { get; set; }
        public bool isEnable { get; set; }
    }
}
