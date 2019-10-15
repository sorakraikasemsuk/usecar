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
}
