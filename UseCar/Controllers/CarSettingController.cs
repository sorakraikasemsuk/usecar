using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Repositories;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class CarSettingController : Controller
    {
        readonly CarSettingRepository carSettingRepository;
        public CarSettingController(CarSettingRepository carSettingRepository)
        {
            this.carSettingRepository = carSettingRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region for brand
        public IActionResult Brand()
        {
            return View();
        }
        public JsonResult GetDatatableBrand(BrandFilter filter)
        {
            return Json(carSettingRepository.GetDatatableBrand(filter));
        }
        public JsonResult GetBrandById(int brandId)
        {
            return Json(carSettingRepository.GetBrandById(brandId));
        }
        [HttpPost]
        public JsonResult CreateBrand(BrandViewModel data)
        {
            return Json(carSettingRepository.CreateBrand(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteBrand(int brandId)
        {
            return Json(carSettingRepository.DeleteBrand(brandId));
        }
        public bool CheckBrandName(int brandId, string brandName)
        {
            return carSettingRepository.CheckBrandName(brandId, brandName);
        }
        #endregion
        #region for generation
        public IActionResult Generation(int brandId)
        {
            var brand = carSettingRepository.GetBrandById(brandId);
            ViewBag.brandId = brand.brandId;
            ViewBag.brandName = brand.brandName;
            return View();
        }
        public JsonResult GetDatatableGeneration(GenerationFilter filter)
        {
            return Json(carSettingRepository.GetDatatableGeneration(filter));
        }
        public JsonResult GetGenerationById(int brandId, int generationId)
        {
            return Json(carSettingRepository.GetGenerationById(brandId, generationId));
        }
        [HttpPost]
        public JsonResult CreateGeneration(GenerationViewModel data)
        {
            return Json(carSettingRepository.CreateGeneration(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteGeneration(int brandId, int generationId)
        {
            return Json(carSettingRepository.DeleteGeneration(brandId, generationId));
        }
        public bool CheckGenerationName(int brandId, int generationId, string generationName)
        {
            return carSettingRepository.CheckGenerationName(brandId, generationId, generationName);
        }
        #endregion
        public IActionResult Face(int brandId,int generationId)
        {
            return View();
        }
        public IActionResult SubFace(int brandId,int generationId,int faceId)
        {
            return View();
        }
        public IActionResult Gear()
        {
            return View();
        }
        public IActionResult CapacityEngine()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult Seat()
        {
            return View();
        }
        public IActionResult Type()
        {
            return View();
        }
        public IActionResult Nature(int typeId)
        {
            return View();
        }
        public IActionResult Option()
        {
            return View();
        }
        public IActionResult DriveSystem()
        {
            return View();
        }
        public IActionResult Color()
        {
            return View();
        }
        public IActionResult EngineType()
        {
            return View();
        }
    }
}