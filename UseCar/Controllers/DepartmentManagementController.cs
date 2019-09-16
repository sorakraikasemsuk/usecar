﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Repositories;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class DepartmentManagementController : Controller
    {
        readonly DepartmentManagementRepository departmentManagementRepository;
        public DepartmentManagementController(DepartmentManagementRepository departmentManagementRepository)
        {
            this.departmentManagementRepository = departmentManagementRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetDatatable(DepartmentManagementFilter filter)
        {
            return Json(departmentManagementRepository.GetDatatable(filter));
        }
        public JsonResult GetDepartmentId(int departmentId)
        {
            return Json(departmentManagementRepository.GetDepartmentById(departmentId));
        }
        public JsonResult Create(DepartmentManagementViewModel data)
        {
            return Json(departmentManagementRepository.Create(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult Delete(int departmentId)
        {
            return Json(departmentManagementRepository.Delete(departmentId));
        }
    }
}