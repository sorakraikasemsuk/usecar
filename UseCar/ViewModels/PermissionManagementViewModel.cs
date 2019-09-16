using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class PermissionManagementViewModel
    {
    }
    public class MenuViewModel
    {
        public int menuId { get; set; }
        public string menuName { get; set; }
        public int ord { get; set; }
    }
    public class DepartmentMenuPermissionViewModel
    {
        public int menuPermissionId { get; set; }
        public int menuId { get; set; }
        public string permission { get; set; }
        public string permissionName { get; set; }
        public int ord { get; set; }
        public int departmentMenuPermissionId { get; set; }
    }
    public class DepartmentMenuPermissionParam
    {
        public int departmentId { get; set; }
        public string menuPermissionId { get; set; }
    }
}
