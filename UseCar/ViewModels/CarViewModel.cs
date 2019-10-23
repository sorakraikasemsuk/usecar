using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class CarViewModel
    {
        public int carId { get; set; }
        public string code { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public int generationId { get; set; }
        public string generationName { get; set; }
        public int faceId { get; set; }
        public string faceName { get; set; }
        public int subfaceId { get; set; }
        public string subfaceName { get; set; }
        public int carStatusId { get; set; }
        public string statusName { get; set; }
        public string registerNumber { get; set; }
        public decimal buyPrice { get; set; }
        public decimal sellPrice { get; set; }
        public string fileName { get; set; }
    }
    public class CarFilter
    {
        public int branchId { get; set; }
        public int brandId { get; set; }
        public int generationId { get; set; }
        public int faceId { get; set; }
        public int subfaceId { get; set; }
        public int carStatusId { get; set; }
        public string registerNumber { get; set; }
    }
}
