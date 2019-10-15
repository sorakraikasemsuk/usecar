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
    public class CheckupCarController : Controller
    {
        readonly SharedData shared;
        readonly CheckupCarRepository checkupCarRepository;
        public CheckupCarController(SharedData shared, CheckupCarRepository checkupCarRepository)
        {
            this.shared = shared;
            this.checkupCarRepository = checkupCarRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetDatatable(CheckupCarDatatableFilter filter)
        {
            return Json(checkupCarRepository.GetDatatable(filter));
        }
        public JsonResult CheckupItem()
        {
            return Json(shared.CheckupData());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> Create(CheckupCarViewModel data)
        {
            return Json(await checkupCarRepository.Create(data));
        }
    }
}