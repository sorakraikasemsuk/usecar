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
}
