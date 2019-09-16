using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class DepartmentManagementViewModel
    {
        public int departmentId { get; set; }
        public string departmentName { get; set; }
        public DateTime updateDate { get; set; }
        public int userInDep { get; set; }
    }
    public class DepartmentManagementFilter
    {
        public string departmentName { get; set; }
    }
}
