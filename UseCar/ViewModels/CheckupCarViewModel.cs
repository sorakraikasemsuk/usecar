using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class CheckupCarViewModel
    {
        public int carCheckupId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int carId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string carDetail { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string checkupDateHidden { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int checkupBy { get; set; }
        public string remark { get; set; }
        public List<CheckupCarDetail> checkupDetail { get; set; }
        public List<IFormFile> files { get; set; }
        public List<int> deleteFile { get; set; }
        public List<ImageDisplay> imageDisplay { get; set; }
    }
    public class CheckupCarDetail
    {
        public int checkupId { get; set; }
    }
    public class CheckupCarDatatableViewModel
    {
        public int carCheckupId { get; set; }
        public int carId { get; set; }
        public string code { get; set; }
        public DateTime checkupDate { get; set; }
        public int checkupBy { get; set; }
        public string checkupByName { get; set; }
        public string fileName { get; set; }
        public string registerNumber { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public int generationId { get; set; }
        public string generationName { get; set; }
        public int faceId { get; set; }
        public string faceName { get; set; }
        public int subfaceId { get; set; }
        public string subfaceName { get; set; }
    }
    public class CheckupCarDatatableFilter
    {
        public int branchId { get; set; }
        public int brandId { get; set; }
        public int generationId { get; set; }
        public int faceId { get; set; }
        public int subfaceId { get; set; }
        public string checkupDate { get; set; }
        public string registerNumber { get; set; }
    }
}
