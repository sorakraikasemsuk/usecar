using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Helper;

namespace UseCar.Controllers
{
    public class SharedDataController : Controller
    {
        readonly SharedData shared;
        readonly DropdownList dropdownList;
        public SharedDataController(SharedData shared, DropdownList dropdownList)
        {
            this.shared = shared;
            this.dropdownList = dropdownList;
        }
        public JsonResult CarSearch(SearchCarFilter filter)
        {
            return Json(shared.CarSearch(filter));
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
            return Json(shared.OptionData());
        }
    }
}