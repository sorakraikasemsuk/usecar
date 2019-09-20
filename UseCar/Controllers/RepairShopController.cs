using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Repositories;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class RepairShopController : Controller
    {
        readonly RepairShopRepository repairShopRepository;
        public RepairShopController(RepairShopRepository repairShopRepository)
        {
            this.repairShopRepository = repairShopRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Shop()
        {
            return View();
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