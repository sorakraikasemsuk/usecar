using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car_Somchai.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UseCar.Helper;
using UseCar.Models;

namespace Car_Somchai.Controllers
{
    public class LoginController : Controller
    {
        readonly UseCarDBContext context;
        public LoginController(UseCarDBContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel data)
        {
            var user = (from a in context.user
                        where a.isEnable
                        && a.userName == data.Username
                        && GeneratePassword.PasswordCheck(data.Password, a.salt, a.password)
                        select a).FirstOrDefault();
            if (user == null)
            {
                return Json(new ResponseResult
                {
                    code = ResponseCode.error,
                    message = "Username or Password Incorrect!"
                });
            }
            else
            {
                if (!user.isActive)
                {
                    return Json(new ResponseResult
                    {
                        code = ResponseCode.error,
                        message = "Username InActive"
                    });
                }
                else
                {
                    HttpContext.Session.SetString(Session.userId, user.userId.ToString());
                    HttpContext.Session.SetString(Session.firstName, user.firstName);
                    HttpContext.Session.SetString(Session.lastName, user.lastName);
                    var menuPermission = (from a in context.permission
                                          join b in context.m_menupermission on a.menuPermissionId equals b.menuPermissionId
                                          join c in context.m_menu on b.menuId equals c.menuId
                                          where a.departmentId == user.departmentId
                                          && b.isEnable && b.permission == Permission.view
                                          && c.isEnable
                                          select new
                                          {
                                              b.menuId,
                                              c.menuName,
                                              c.menuControllerName,
                                              c.icon,
                                              c.ord
                                          }).OrderBy(o => o.ord).ToList();
                    HttpContext.Session.SetString(Session.menuPermission, JsonConvert.SerializeObject(menuPermission));
                    return Json(new ResponseResult
                    {
                        code = ResponseCode.ok,
                        message = "/Home"
                    });
                }
            }
        }
    }
}