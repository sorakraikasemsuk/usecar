using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Repositories;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class CarController : Controller
    {
        readonly CarRepository carRepository;
        public CarController(CarRepository carRepository)
        {
            this.carRepository = carRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetDatatable(CarFilter filter)
        {
            return Json(carRepository.GetDatatable(filter));
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}