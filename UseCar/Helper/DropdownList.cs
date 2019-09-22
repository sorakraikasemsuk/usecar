using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Models;

namespace UseCar.Helper
{
    public class DropdownList
    {
        readonly UseCarDBContext context;
        public DropdownList(UseCarDBContext context)
        {
            this.context = context;
        }
        public List<SelectListItem> DeaprtmentAll()
        {
            return (from a in context.department
                    where a.isEnable
                    select new SelectListItem {
                        Value=a.departmentId.ToString(),
                        Text=a.departmentName
                    }).ToList();
        }
        public List<SelectListItem> CategoryShopAll()
        {
            return (from a in context.categoryshop
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.categoryShopId.ToString(),
                        Text = a.categoryShopName
                    }).ToList();
        }
    }
}
