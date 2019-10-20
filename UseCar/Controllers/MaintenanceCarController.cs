using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Repositories;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class MaintenanceCarController : Controller
    {
        readonly MaintenanceCarRepository maintenanceCarRepository;
        public MaintenanceCarController(MaintenanceCarRepository maintenanceCarRepository)
        {
            this.maintenanceCarRepository = maintenanceCarRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetDatatable(MaintenanceCarDatatableFilter filter)
        {
            return Json(maintenanceCarRepository.GetDatatable(filter));
        }
        public IActionResult Create(int maintenanceId)
        {
            MaintenanceCarViewModel data = new MaintenanceCarViewModel();
            if (maintenanceId == 0)
            {
                data.code = "MA" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "-XXXX";
            }
            return View(data);
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public  async Task<JsonResult> Create(MaintenanceCarViewModel data)
        {
            return Json(await maintenanceCarRepository.Create(data));
        }
    }
}