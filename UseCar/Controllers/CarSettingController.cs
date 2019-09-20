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
        #region for capacityEngine
        public IActionResult CapacityEngine()
        {
            return View();
        }
        public JsonResult GetDatatableCapacityEngine(CapacityEngineFilter filter)
        {
            return Json(carSettingRepository.GetDatatableCapacityEngine(filter));
        }
        public JsonResult GetCapacityEngineById(int capacityEngineId)
        {
            return Json(carSettingRepository.GetCapacityEngineById(capacityEngineId));
        }
        [HttpPost]
        public JsonResult CreateCapacityEngine(CapacityEngineViewModel data)
        {
            return Json(carSettingRepository.CreateCapacityEngine(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteCapacityEngine(int capacityEngineId)
        {
            return Json(carSettingRepository.DeleteCapacityEngine(capacityEngineId));
        }
        public bool CheckCapacityEngineName(int capacityEngineId, string capacityEngineName)
        {
            return carSettingRepository.CheckCapacityEngineName(capacityEngineId, capacityEngineName);
        }
        #endregion
        #region for category
        public IActionResult Category()
        {
            return View();
        }
        public JsonResult GetDatatableCategory(CategoryFilter filter)
        {
            return Json(carSettingRepository.GetDatatableCategory(filter));
        }
        public JsonResult GetCategoryById(int categoryId)
        {
            return Json(carSettingRepository.GetCategoryById(categoryId));
        }
        [HttpPost]
        public JsonResult CreateCategory(CategoryViewModel data)
        {
            return Json(carSettingRepository.CreateCategory(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteCategory(int categoryId)
        {
            return Json(carSettingRepository.DeleteCategory(categoryId));
        }
        public bool CheckCategoryName(int categoryId, string categoryName)
        {
            return carSettingRepository.CheckCategoryName(categoryId, categoryName);
        }
        #endregion
        #region for seat
        public IActionResult Seat()
        {
            return View();
        }
        public JsonResult GetDatatableSeat(SeatFilter filter)
        {
            return Json(carSettingRepository.GetDatatableSeat(filter));
        }
        public JsonResult GetSeatById(int seatId)
        {
            return Json(carSettingRepository.GetSeatById(seatId));
        }
        [HttpPost]
        public JsonResult CreateSeat(SeatViewModel data)
        {
            return Json(carSettingRepository.CreateSeat(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteSeat(int seatId)
        {
            return Json(carSettingRepository.DeleteSeat(seatId));
        }
        public bool CheckSeatName(int seatId, string seatName)
        {
            return carSettingRepository.CheckSeatName(seatId, seatName);
        }
        #endregion
        #region for type
        public IActionResult Type()
        {
            return View();
        }
        public JsonResult GetDatatableType(TypeFilter filter)
        {
            return Json(carSettingRepository.GetDatatableType(filter));
        }
        public JsonResult GetTypeById(int typeId)
        {
            return Json(carSettingRepository.GetTypeById(typeId));
        }
        [HttpPost]
        public JsonResult CreateType(TypeViewModel data)
        {
            return Json(carSettingRepository.CreateType(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteType(int typeId)
        {
            return Json(carSettingRepository.DeleteType(typeId));
        }
        public bool CheckTypeName(int typeId, string typeName)
        {
            return carSettingRepository.CheckTypeName(typeId, typeName);
        }
        #endregion
        public IActionResult Nature(int typeId)
        {
            return View();
        }
        #region for option
        public IActionResult Option()
        {
            return View();
        }
        public JsonResult GetDatatableOption(OptionFilter filter)
        {
            return Json(carSettingRepository.GetDatatableOption(filter));
        }
        public JsonResult GetOptionById(int optionId)
        {
            return Json(carSettingRepository.GetOptionById(optionId));
        }
        [HttpPost]
        public JsonResult CreateOption(OptionViewModel data)
        {
            return Json(carSettingRepository.CreateOption(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteOption(int optionId)
        {
            return Json(carSettingRepository.DeleteOption(optionId));
        }
        public bool CheckOptionName(int optionId, string optionName)
        {
            return carSettingRepository.CheckOptionName(optionId, optionName);
        }
        #endregion
        #region for driveSystem
        public IActionResult DriveSystem()
        {
            return View();
        }
        public JsonResult GetDatatableDriveSystem(DriveSystemFilter filter)
        {
            return Json(carSettingRepository.GetDatatableDriveSystem(filter));
        }
        public JsonResult GetDriveSystemById(int driveSystemId)
        {
            return Json(carSettingRepository.GetDriveSystemById(driveSystemId));
        }
        [HttpPost]
        public JsonResult CreateDriveSystem(DriveSystemViewModel data)
        {
            return Json(carSettingRepository.CreateDriveSystem(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteDriveSystem(int driveSystemId)
        {
            return Json(carSettingRepository.DeleteDriveSystem(driveSystemId));
        }
        public bool CheckDriveSystemName(int driveSystemId, string driveSystemName)
        {
            return carSettingRepository.CheckDriveSystemName(driveSystemId, driveSystemName);
        }
        #endregion
        #region for color
        public IActionResult Color()
        {
            return View();
        }
        public JsonResult GetDatatableColor(ColorFilter filter)
        {
            return Json(carSettingRepository.GetDatatableColor(filter));
        }
        public JsonResult GetColorById(int colorId)
        {
            return Json(carSettingRepository.GetColorById(colorId));
        }
        [HttpPost]
        public JsonResult CreateColor(ColorViewModel data)
        {
            return Json(carSettingRepository.CreateColor(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteColor(int colorId)
        {
            return Json(carSettingRepository.DeleteColor(colorId));
        }
        public bool CheckColorName(int colorId, string colorName)
        {
            return carSettingRepository.CheckColorName(colorId, colorName);
        }
        #endregion
        #region for engineType
        public IActionResult EngineType()
        {
            return View();
        }
        public JsonResult GetDatatableEngineType(EngineTypeFilter filter)
        {
            return Json(carSettingRepository.GetDatatableEngineType(filter));
        }
        public JsonResult GetEngineTypeById(int engineTypeId)
        {
            return Json(carSettingRepository.GetEngineTypeById(engineTypeId));
        }
        [HttpPost]
        public JsonResult CreateEngineType(EngineTypeViewModel data)
        {
            return Json(carSettingRepository.CreateEngineType(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult DeleteEngineType(int engineTypeId)
        {
            return Json(carSettingRepository.DeleteEngineType(engineTypeId));
        }
        public bool CheckEngineTypeName(int engineTypeId, string engineTypeName)
        {
            return carSettingRepository.CheckEngineTypeName(engineTypeId, engineTypeName);
        }
        #endregion
    }
}