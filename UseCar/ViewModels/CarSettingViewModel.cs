using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class CarSettingViewModel
    {
    }
    #region for brand
    public class BrandViewModel
    {
        public int brandId { get; set; }
        public string brandName { get; set; }
        public int carInBrand { get; set; }
    }
    public class BrandFilter
    {
        public string brandName { get; set; }
    }
    #endregion
}
