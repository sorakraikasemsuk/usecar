﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Helper;

namespace UseCar.Controllers
{
    public class ReceiveCarController : Controller
    {
        readonly DropdownList dropdownList;
        public ReceiveCarController(DropdownList dropdownList)
        {
            this.dropdownList = dropdownList;
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
    }
}