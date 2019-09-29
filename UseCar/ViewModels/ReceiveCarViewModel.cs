﻿using Microsoft.AspNetCore.Http;
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
        public string brandName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int generationId { get; set; }
        public string generationName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int faceId { get; set; }
        public string faceName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int subfaceId { get; set; }
        public string subfaceName { get; set; }
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
        public string natureName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int year { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string receiveDateHidden { get; set; }
        public int carStatusId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int receiveCarStatusId { get; set; }
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
        public string registerDateHidden { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string registerNumber { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int provinceId { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int order { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string ownerDateHidden { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string ownerName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string ownerAddress { get; set; }
        public List<IFormFile> files { get; set; }
        public List<int> deleteFile { get; set; }
        public List<ImageDisplay> imageDisplay { get; set; }
    }
    public class ReceiveCarOption
    {
        public int optionId { get; set; }
    }
    public class ImageDisplay
    {
        public int imageId { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }
    public class ReceiveCarDatatableViewModel
    {
        public int carId { get; set; }
        public string code { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public int generationId { get; set; }
        public string generationName { get; set; }
        public int faceId { get; set; }
        public string faceName { get; set; }
        public int subfaceId { get; set; }
        public string subfaceName { get; set; }
        public decimal buyPrice { get; set; }
        public DateTime receiveDate { get; set; }
        public int receiveCarStatusId { get; set; }
        public string statusName { get; set; }
        public string fileName { get; set; }
    }
    public class ReceiveCarDatatableFilter
    {
        
    }
}
