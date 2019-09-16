using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Repositories;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class PermissionManagementController : Controller
    {
        readonly PermissionManagementRepository permissionManagementRepository;
        public PermissionManagementController(PermissionManagementRepository permissionManagementRepository)
        {
            this.permissionManagementRepository = permissionManagementRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetMenu()
        {
            return Json(permissionManagementRepository.GetMenu());
        }
        public JsonResult GetDepartmentMenuPermission(int departmentId)
        {
            return Json(permissionManagementRepository.GetDepartmentMenuPermission(departmentId));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult Create(DepartmentMenuPermissionParam param)
        {
            return Json(permissionManagementRepository.Create(param));
        }
    }
}