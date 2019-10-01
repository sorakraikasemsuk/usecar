using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Repositories;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class CheckupSettingController : Controller
    {
        readonly CheckupSettingRepository checkupSettingRepository;
        public CheckupSettingController(CheckupSettingRepository checkupSettingRepository)
        {
            this.checkupSettingRepository = checkupSettingRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetDatatable(CheckupFilter filter)
        {
            return Json(checkupSettingRepository.GetDatatable(filter));
        }
        public JsonResult GetCheckupById(int checkupId)
        {
            return Json(checkupSettingRepository.GetCheckupById(checkupId));
        }
        [HttpPost]
        public JsonResult Create(CheckupSettingViewModel data)
        {
            return Json(checkupSettingRepository.Create(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult Delete(int checkupId)
        {
            return Json(checkupSettingRepository.Delete(checkupId));
        }
        public bool CheckName(int checkupId, string checkupName)
        {
            return checkupSettingRepository.CheckName(checkupId, checkupName);
        }
    }
}