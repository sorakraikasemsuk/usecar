using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Helper;

namespace UseCar.Filter
{
    public class UseCarActionFilter: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            var controller = context.ActionDescriptor.RouteValues["controller"];
            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString(Session.userId)) && controller != "Login")
            { 
                context.Result = new RedirectResult("/Login");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
        }
    }
}
