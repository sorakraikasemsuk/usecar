using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Models;

namespace UseCar.Helper
{
    public class SharedData
    {
        readonly UseCarDBContext context;
        public SharedData(UseCarDBContext context)
        {
            this.context = context;
        }
        public List<SharedDataOptionViewModel> OptionData()
        {
            return (from a in context.option
                    where a.isEnable
                    select new SharedDataOptionViewModel
                    {
                        optionId = a.optionId,
                        optionName = a.optionName
                    }).ToList();
        }
        public SheardDataVendorViewModel VendorData(int vendorId)
        {
            return (from a in context.vendor
                    where a.isEnable
                    && a.vendorId == vendorId
                    select new SheardDataVendorViewModel
                    {
                        vendorId = a.vendorId,
                        vendorName = a.vendorName,
                        vendorAddress = a.vendorAddress,
                        vendorTel = a.vendorTel,
                        vendorNumber = a.vendorNumber
                    }).FirstOrDefault() ?? new SheardDataVendorViewModel();
        }
    }
    public class SharedDataOptionViewModel
    {
        public int optionId { get; set; }
        public string optionName { get; set; }
    }
    public class SheardDataVendorViewModel
    {
        public int vendorId { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorTel { get; set; }
        public string vendorNumber { get; set; }
    }
}
