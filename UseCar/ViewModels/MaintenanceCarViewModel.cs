using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class MaintenanceCarViewModel
    {
        public int maintenanceId { get; set; }
        public string code { get; set; }
        [Required(ErrorMessage ="กรุณากรอกข้อมูล")]
        public int carId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string carDetail { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int repairShopId { get; set; }
        public string sendDateHidden { get; set; }
        public string receiveDateHidden { get; set; }
        public string remark { get; set; }
        public int maintenanceStatusId { get; set; }
        public List<MaintenanceDetail> details { get; set; }
        public List<IFormFile> files { get; set; }
        public List<int> deleteFile { get; set; }
        public List<ImageDisplay> imageDisplay { get; set; }
    }
    public class MaintenanceDetail
    {
        public int maintenanceDetailId { get; set; }
        public int itemNo { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
    }
}
