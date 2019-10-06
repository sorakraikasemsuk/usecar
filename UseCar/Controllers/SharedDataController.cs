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
        public SharedDataController(SharedData shared)
        {
            this.shared = shared;
        }
        public JsonResult CarSearch(SearchCarFilter filter)
        {
            return Json(shared.CarSearch(filter));
        }
    }
}