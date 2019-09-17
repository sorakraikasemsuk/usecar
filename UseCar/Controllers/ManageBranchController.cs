using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UseCar.Repositories;
using UseCar.ViewModels;

namespace UseCar.Controllers
{
    public class ManageBranchController : Controller
    {
        readonly ManageBranchRepository manageBranchRepository;
        public ManageBranchController(ManageBranchRepository manageBranchRepository)
        {
            this.manageBranchRepository = manageBranchRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetDatatable(ManageBranchFilter filter)
        {
            return Json(manageBranchRepository.GetDatatable(filter));
        }
        public JsonResult GetBranchById(int branchId)
        {
            return Json(manageBranchRepository.GetBranchById(branchId));
        }
        [HttpPost]
        public JsonResult Create(ManageBranchViewModel data)
        {
            return Json(manageBranchRepository.Create(data));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult Delete(int branchId)
        {
            return Json(manageBranchRepository.Delete(branchId));
        }
        public bool CheckBranchName(int branchId,string branchName)
        {
            return manageBranchRepository.CheckBranchName(branchId, branchName);
        }
    }
}