﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Helper;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class ReceiveCarController : Controller
    {
        readonly DropdownList dropdownList;
        readonly SharedData sharedData;
        public ReceiveCarController(DropdownList dropdownList, SharedData sharedData)
        {
            this.dropdownList = dropdownList;
            this.sharedData = sharedData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetGenerationByBrandId(int brandId)
        {
            return Json(dropdownList.GenerationByBrand(brandId));
        }
        public JsonResult GetFaceById(int generationId)
        {
            return Json(dropdownList.FacByGeneration(generationId));
        }
        public JsonResult GetSubFaceByFaceId(int faceId)
        {
            return Json(dropdownList.SubFaceByFace(faceId));
        }
        public JsonResult GetNatureByTypeId(int typeId)
        {
            return Json(dropdownList.NatureByType(typeId));
        }
        public JsonResult GetOption()
        {
            return Json(sharedData.OptionData());
        }
        public JsonResult GetVendor(int vendorId)
        {
            return Json(sharedData.VendorData(vendorId));
        }
        public IActionResult Create(int carId)
        {
            ReceiveCarViewModel car = new ReceiveCarViewModel();
            if (carId == 0)
            {
                car.code = "CAR" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2,'0') + "-XXXX";
            }
            return View(car);
        }
    }
}