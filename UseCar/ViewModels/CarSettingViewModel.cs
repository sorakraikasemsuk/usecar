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
    #region for generation
    public class GenerationViewModel
    {
        public int generationId { get; set; }
        public int brandId { get; set; }
        public string generationName { get; set; }
        public string brandName { get; set; }
        public int carInGeneration { get; set; }
    }
    public class GenerationFilter
    {
        public int brandId { get; set; }
        public string generationName { get; set; }
    }
    #endregion
    #region for face
    public class FaceViewModel
    {
        public int faceId { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public int generationId { get; set; }
        public string generationName { get; set; }
        public string faceName { get; set; }
        public int carInFace { get; set; }
    }
    public class FaceFilter
    {
        public int brandId { get; set; }
        public int generationId { get; set; }
        public string faceName { get; set; }
    }
    #endregion
    #region for subface
    public class SubFaceViewModel
    {
        public int subfaceId { get; set; }
        public int brandId { get; set; }
        public int generationId { get; set; }
        public int faceId { get; set; }
        public string subfaceName { get; set; }
        public int carInSubface { get; set; }
    }
    public class SubFaceFilter
    {
        public int brandId { get; set; }
        public int generationId { get; set; }
        public int faceId { get; set; }
        public string subfaceName { get; set; }
    }
    #endregion
    #region for gear
    public class GearViewModel
    {
        public int gearId { get; set; }
        public string gearName { get; set; }
        public int carInGear { get; set; }
    }
    public class GearFilter
    {
        public string gearName { get; set; }
    }
    #endregion
    #region for capacityEngine
    public class CapacityEngineViewModel
    {
        public int capacityEngineId { get; set; }
        public string capacityEngineName { get; set; }
        public int carInCapacityEngine { get; set; }
    }
    public class CapacityEngineFilter
    {
        public string capacityEngineName { get; set; }
    }
    #endregion
    #region for category
    public class CategoryViewModel
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public int carInCate { get; set; }
    }
    public class CategoryFilter
    {
        public string categoryName { get; set; }
    }
    #endregion
    #region for seat
    public class SeatViewModel
    {
        public int seatId { get; set; }
        public string seatName { get; set; }
        public int carInSeat { get; set; }
    }
    public class SeatFilter
    {
        public string seatName { get; set; }
    }
    #endregion
    #region fot option
    public class OptionViewModel
    {
        public int optionId { get; set; }
        public string optionName { get; set; }
        public int carInOption { get; set; }
    }
    public class OptionFilter
    {
        public string optionName { get; set; }
    }
    #endregion
}
