using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class VendorViewModel
    {
        public int vendorId { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorTel { get; set; }
        public string vendorNumber { get; set; }
        public int carInVendor { get; set; }
    }
    public class VendorFilter
    {
        public string vendorName { get; set; }
    }
}
