using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Models;

namespace UseCar.ViewComponents
{
    public class CarSearchViewComponent:ViewComponent
    {
        readonly UseCarDBContext context;
        public CarSearchViewComponent(UseCarDBContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke(int carStatusId)
        {
            var car = (from a in context.car
                       where a.isEnable
                       && a.carStatusId == carStatusId
                       select new CarSearchViewModel
                       {
                           carId = a.carId,
                           code = a.code
                       }).ToList();
            return View(car);
        }
    }
    public class CarSearchViewModel
    {
        public int carId { get; set; }
        public string code { get; set; }
    }
}
