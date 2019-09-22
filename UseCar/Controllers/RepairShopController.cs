using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Helper;
using UseCar.Repositories;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class RepairShopController : Controller
    {
        readonly RepairShopRepository repairShopRepository;
        readonly DropdownList dropdownList;
        public RepairShopController(RepairShopRepository repairShopRepository, DropdownList dropdownList)
        {
            this.repairShopRepository = repairShopRepository;
            this.dropdownList = dropdownList;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Shop()
        {
            ViewBag.CategoryShopAll = dropdownList.CategoryShopAll();
            return View();
        }
        public JsonResult GetDatatableRepairShop(RepairShopFilter filter)
        {
            return Json(repairShopRepository.GetDatatableRepairShop(filter));
        }
        public JsonResult GetRepairShopById(int repairShopId)
        {
            return Json(repairShopRepository.GetRepairShopById(repairShopId));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult CreateRepairShop(RepairShopViewModel data)
        {
            return Json(repairShopRepository.CreateRepairShop(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteRepairShop(int repairShopId)
        {
            return Json(repairShopRepository.DeleteRepairShop(repairShopId));
        }
        public bool CheckRepairShopName(int repairShopId, string repairShopName)
        {
            return repairShopRepository.CheckRepairShopName(repairShopId, repairShopName);
        }
        #region for categoryShop
        public IActionResult CategoryShop()
        {
            return View();
        }
        public JsonResult GetDatatableCategoryShop(CategoryShopFilter filter)
        {
            return Json(repairShopRepository.GetDatatableCategoryShop(filter));
        }
        public JsonResult GetCategoryShopById(int categoryShopId)
        {
            return Json(repairShopRepository.GetCategoryShopById(categoryShopId));
        }
        [HttpPost]
        public JsonResult CreateCategoryShop(CategoryShopViewModel data)
        {
            return Json(repairShopRepository.CreateCategoryShop(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteCategoryShop(int categoryShopId)
        {
            return Json(repairShopRepository.DeleteCategoryShop(categoryShopId));
        }
        public bool CheckCategoryShopName(int categoryShopId, string categoryShopName)
        {
            return repairShopRepository.CheckCategoryShopName(categoryShopId, categoryShopName);
        }
        #endregion
    }
}