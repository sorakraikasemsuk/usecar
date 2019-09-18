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
        #region for face
        public IActionResult Face(int brandId,int generationId)
        {
            var generation = carSettingRepository.GetGenerationById(brandId, generationId);
            ViewBag.brandId = generation.brandId;
            ViewBag.brandName = generation.brandName;
            ViewBag.generationId = generation.generationId;
            ViewBag.generationName = generation.generationName;
            return View();
        }
        public JsonResult GetDatatableFace(FaceFilter filter)
        {
            return Json(carSettingRepository.GetDatatableFace(filter));
        }
        public JsonResult GetFaceById(int brandId, int generationId, int faceId)
        {
            return Json(carSettingRepository.GetFaceById(brandId, generationId, faceId));
        }
        [HttpPost]
        public JsonResult CreateFace(FaceViewModel data)
        {
            return Json(carSettingRepository.CreateFace(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteFace(int brandId, int generationId, int faceId)
        {
            return Json(carSettingRepository.DeleteFace(brandId, generationId, faceId));
        }
        public bool CheckFaceName(int brandId, int generationId, int faceId, string faceName)
        {
            return carSettingRepository.CheckFaceName(brandId, generationId, faceId, faceName);
        }
        #endregion
        #region for subface
        public IActionResult SubFace(int brandId,int generationId,int faceId)
        {
            var face = carSettingRepository.GetFaceById(brandId, generationId, faceId);
            ViewBag.brandId = face.brandId;
            ViewBag.brandName = face.brandName;
            ViewBag.generationId = face.generationId;
            ViewBag.generationName = face.generationName;
            ViewBag.faceId = face.faceId;
            ViewBag.faceName = face.faceName;
            return View();
        }
        public JsonResult GetDatatableSubface(SubFaceFilter filter)
        {
            return Json(carSettingRepository.GetDatatableSubface(filter));
        }
        public JsonResult GetSubfaceById(int brandId, int generationId, int faceId, int subfaceId)
        {
            return Json(carSettingRepository.GetSubfaceById(brandId, generationId, faceId, subfaceId));
        }
        [HttpPost]
        public JsonResult CreateSubface(SubFaceViewModel data)
        {
            return Json(carSettingRepository.CreateSubface(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteSubface(int brandId, int generationId, int faceId, int subfaceId)
        {
            return Json(carSettingRepository.DeleteSubface(brandId, generationId, faceId, subfaceId));
        }
        public bool CheckSubfaceName(int brandId, int generationId, int faceId, int subfaceId, string subfaceName)
        {
            return carSettingRepository.CheckSubfaceName(brandId, generationId, faceId, subfaceId, subfaceName);
        }
        #endregion
        #region for gear
        public IActionResult Gear()
        {
            return View();
        }
        public JsonResult GetDatatableGear(GearFilter filter)
        {
            return Json(carSettingRepository.GetDatatableGear(filter));
        }
        public JsonResult GetGearById(int gearId)
        {
            return Json(carSettingRepository.GetGearById(gearId));
        }
        [HttpPost]
        public JsonResult CreateGear(GearViewModel data)
        {
            return Json(carSettingRepository.CreateGear(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteGear(int gearId)
        {
            return Json(carSettingRepository.DeleteGear(gearId));
        }
        public bool CheckGearName(int gearId, string gearName)
        {
            return carSettingRepository.CheckGearName(gearId, gearName);
        }
        #endregion
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