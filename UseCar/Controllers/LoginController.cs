using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car_Somchai.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Car_Somchai.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel data)
        {
            return Redirect("/Home");
        }
    }
}