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
    public class CarDetailViewModel
    {
        public int carId { get; set; }
        public string code { get; set; }
        public int branchId { get; set; }
        public string branchName { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public int generationId { get; set; }
        public string generationName { get; set; }
        public int faceId { get; set; }
        public string faceName { get; set; }
        public int subfaceId { get; set; }
        public string subfaceName { get; set; }
        public string serialNumber { get; set; }
        public string engineNumber { get; set; }
        public string mileNumber { get; set; }
        public string brandEngine { get; set; }
        public string gasNumber { get; set; }
        public string weight { get; set; }
        public int colorId { get; set; }
        public string colorName { get; set; }
        public int gearId { get; set; }
        public string gearName { get; set; }
        public int seatId { get; set; }
        public string seatName { get; set; }
        public int driveSystemId { get; set; }
        public string driveSystemName { get; set; }
        public int engineTypeId { get; set; }
        public string engineTypeName { get; set; }
        public int capacityEngineId { get; set; }
        public string capacityEngineName { get; set; }
        public int typeId { get; set; }
        public string typeName { get; set; }
        public int natureId { get; set; }
        public string natureName { get; set; }
        public int year { get; set; }
        public string remark { get; set; }
        public List<CarOption> options { get; set; }
        public List<CarRegister> registers { get; set; }
        public List<CarImage> images { get; set; }
        public List<CarHistory> histories { get; set; }
    }
    public class CarOption
    {
        public int optionId { get; set; }
        public string optionName { get; set; }
    }
    public class CarRegister
    {
        public int registerId { get; set; }
        public DateTime registerDate { get; set; }
        public string registerNumber { get; set; }
        public int provinceId { get; set; }
        public string provinceName { get; set; }
        public List<CarOwner> owners { get; set; }
    }
    public class CarOwner
    {
        public int ownerId { get; set; }
        public int order { get; set; }
        public DateTime ownerDate { get; set; }
        public string ownerName { get; set; }
        public string ownerAddress { get; set; }
    }
    public class CarImage
    {
        public int imageId { get; set; }
        public string name { get; set; }
        public int menuId { get; set; }
        public string image { get; set; }
    }
    public class CarHistory
    {
        public int historyId { get; set; }
        public int menuId { get; set; }
        public string menuName { get; set; }
        public string icon { get; set; }
        public int statusId { get; set; }
        public string statusName { get; set; }
        public DateTime createDate { get; set; }
        public int createUserId { get; set; }
        public string createUserName { get; set; }
    }
}
