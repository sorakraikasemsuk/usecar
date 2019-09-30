using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Models;

namespace UseCar.Repositories
{
    public class MaintenanceCarRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        public MaintenanceCarRepository(UseCarDBContext context,IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
        }
    }
}
