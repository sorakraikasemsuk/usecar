using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class MaintenanceCarViewModelcs
    {
        public int maintenanceId { get; set; }
        public string code { get; set; }
        [Required(ErrorMessage ="กรุณากรอกข้อมูล")]
        public int carId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int repairShopId { get; set; }
        public string sendDate { get; set; }
        public string receiveDate { get; set; }
        public string remark { get; set; }
        public List<MaintenanceDetail> details { get; set; }
        public List<IFormFile> files { get; set; }
    }
    public class MaintenanceDetail
    {
        public int itemNo { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
    }
}
