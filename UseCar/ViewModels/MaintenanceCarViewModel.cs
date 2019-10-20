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
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int sendById { get; set; }
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
    public class MaintenanceCarDatatableViewModel
    {
        public int maintenanceId { get; set; }
        public string code { get; set; }
        public int carId { get; set; }
        public string carCode { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public int generationId { get; set; }
        public string generationName { get; set; }
        public int faceId { get; set; }
        public string faceName { get; set; }
        public int subfaceId { get; set; }
        public string subfaceName { get; set; }
        public DateTime sendDate { get; set; }
        public DateTime receiveDate { get; set; }
        public int maintenanceStatusId { get; set; }
        public string maintenanceStatusName { get; set; }
        public string fileName { get; set; }
        public string registerNumber { get; set; }
        public int sendById { get; set; }
        public string sendByName { get; set; }
    }
    public class MaintenanceCarDatatableFilter
    {
        public int branchId { get; set; }
        public int brandId { get; set; }
        public int generationId { get; set; }
        public int faceId { get; set; }
        public int subfaceId { get; set; }
        public int maintenanceStatusId { get; set; }
        public string sendDate { get; set; }
        public string receiveDate { get; set; }
        public string registerNumber { get; set; }
    }
}
