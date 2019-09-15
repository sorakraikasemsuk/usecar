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
    public class UserManagementController : Controller
    {
        readonly DropdownList dropdownList;
        readonly UserManagementRepository userManagementRepository;
        public UserManagementController(DropdownList dropdownList, UserManagementRepository userManagementRepository)
        {
            this.dropdownList = dropdownList;
            this.userManagementRepository = userManagementRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int userId)
        {
            UserManagementViewModel user = new UserManagementViewModel();
            if (userId == 0)
            {
                user.code = "USER-XXXX";
            }
            else
            {

            }
            ViewBag.Department = dropdownList.DeaprtmentAll();
            return View(user);
        }
        [HttpPost]
        public IActionResult Create(UserManagementViewModel data)
        {
            return Json(userManagementRepository.Create(data));
        }
        public IActionResult Detail()
        {
            return View();
        }
    }
}