using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class ReceiveCarViewModel
    {
        public int carId { get; set; }
        public string code { get; set; }
        [Required(ErrorMessage ="กรุณากรอกข้อมูล")]
        public int branchId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int categoryId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int brandId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int generationId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int faceId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int subfaceId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string serialNumber { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string engineNumber { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string mileNumber { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string brandEngine { get; set; }
        public string gasNumber { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string weight { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int colorId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int gearId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int seatId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int driveSystemId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int engineTypeId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int capacityEngineId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int typeId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int natureId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int year { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public DateTime receiveDateHidden { get; set; }
        public int carStatusId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int carProcessId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int vendorId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorTel { get; set; }
        public string vendorNumber { get; set; }
        public string remark { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public decimal buyPrice { get; set; }
        public List<ReceiveCarOption> options { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public DateTime registerDateHidden { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string registerNumber { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int provinceId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int order { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public DateTime ownerDateHidden { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string ownerName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string ownerAddress { get; set; }
        public List<IFormFile> files { get; set; }
    }
    public class ReceiveCarOption
    {
        public int optionId { get; set; }
    }
}
