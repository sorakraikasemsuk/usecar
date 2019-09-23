using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class car
    {
        public int carId { get; set; }
        public string code { get; set; }
        public int branchId { get; set; }
        public int categoryId { get; set; }
        public int brandId { get; set; }
        public int generationId { get; set; }
        public int faceId { get; set; }
        public int subfaceId { get; set; }
        public string serialNumber { get; set; }
        public string engineNumber { get; set; }
        public string mileNumber { get; set; }
        public string brandEngine { get; set; }
        public string gasNumber { get; set; }
        public string weight { get; set; }
        public int colorId { get; set; }
        public int gearId { get; set; }
        public int seatId { get; set; }
        public int driveSystemId { get; set; }
        public int engineTypeId { get; set; }
        public int capacityEngineId { get; set; }
        public int typeId { get; set; }
        public int natureId { get; set; }
        public int year { get; set; }
        public DateTime receiveDate { get; set; }
        public int carStatusId { get; set; }
        public int carProcessId { get; set; }
        public int vendorId { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorTel { get; set; }
        public string vendorNumber { get; set; }
        public string remark { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
        public decimal buyPrice { get; set; }
        public decimal? sellPrice { get; set; }
    }
}
