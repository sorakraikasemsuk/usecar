﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UseCar.Controllers
{
    public class DepartmentManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}