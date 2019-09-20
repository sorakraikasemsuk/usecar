using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class RepairShopViewModel
    {
    }
    #region for categoryShop
    public class CategoryShopViewModel
    {
        public int categoryShopId { get; set; }
        public string categoryShopName { get; set; }
        public int shopInCate { get; set; }
    }
    public class CategoryShopFilter
    {
        public string categoryShopName { get; set; }
    }
    #endregion
}
