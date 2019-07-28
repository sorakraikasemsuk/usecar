using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UseCar.Controllers
{
    public class CarSettingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Brand()
        {
            return View();
        }
        public IActionResult Generation(int brandId)
        {
            return View();
        }
        public IActionResult Face(int brandId,int generationId)
        {
            return View();
        }
        public IActionResult SubFace(int brandId,int generationId,int faceId)
        {
            return View();
        }
    }
}