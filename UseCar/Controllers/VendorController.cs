using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Repositories;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class VendorController : Controller
    {
        readonly VendorRepository vendorRepository;
        public VendorController(VendorRepository vendorRepository)
        {
            this.vendorRepository = vendorRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetDatatable(VendorFilter filter)
        {
            return Json(vendorRepository.GetDatatable(filter));
        }
        public JsonResult GetVendorById(int vendorId)
        {
            return Json(vendorRepository.GetVendorById(vendorId));
        }
        [HttpPost]
        public JsonResult Create(VendorViewModel data)
        {
            return Json(vendorRepository.Create(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult Delete(int vendorId)
        {
            return Json(vendorRepository.Delete(vendorId));
        }
        public bool CheckVendorName(int vendorId, string vendorName)
        {
            return vendorRepository.CheckVendorName(vendorId, vendorName);
        }
    }
}