using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    #region for repairShop
    public class RepairShopViewModel
    {
        public int repairShopId { get; set; }
        public string repairShopName { get; set; }
        public string repairShopAddress { get; set; }
        public string tel { get; set; }
        public string fax { get; set; }
        public int categoryShopId { get; set; }
        public string categoryShopName { get; set; }
        public int maInShop { get; set; }
        public List<ContactRepairShop> contactRepairShops { get; set; }
    }
    public class ContactRepairShop
    {
        public int contactRepairShopId { get; set; }
        public string contactName { get; set; }
        public string tel { get; set; }
        public string remark { get; set; }
    }
    public class RepairShopFilter
    {
        public int categoryShopId { get; set; }
        public string repairShopName { get; set; }
    }
    #endregion
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
