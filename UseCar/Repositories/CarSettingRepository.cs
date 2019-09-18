using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Helper;
using UseCar.Models;
using UseCar.ViewModels;

namespace UseCar.Repositories
{
    public class CarSettingRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        public CarSettingRepository(UseCarDBContext context,IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
        }
        #region for brand
        public List<BrandViewModel> GetDatatableBrand(BrandFilter filter)
        {
            return (from a in context.brand
                    where a.isEnable
                    && (a.brandName.Contains(filter.brandName) || filter.brandName == null)
                    select new BrandViewModel
                    {
                        brandId = a.brandId,
                        brandName = a.brandName,
                        carInBrand = 0
                    }).ToList();
        }
        public BrandViewModel GetBrandById(int brandId)
        {
            return (from a in context.brand
                    where a.isEnable
                    && a.brandId == brandId
                    select new BrandViewModel
                    {
                        brandId = a.brandId,
                        brandName = a.brandName
                    }).FirstOrDefault() ?? new BrandViewModel();
        }
        public ResponseResult CreateBrand(BrandViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.brandId == 0)
                    {
                        brand brand = new brand
                        {
                            brandName = data.brandName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.brand.Add(brand);
                        context.SaveChanges();
                    }
                    else
                    {
                        var brand = (from a in context.brand
                                     where a.isEnable
                                     && a.brandId == data.brandId
                                     select a).FirstOrDefault();
                        brand.brandName = data.brandName;
                        brand.updateDate = DateTime.Now;
                        brand.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        brand.isEnable = true;
                        context.SaveChanges();
                    }
                    Transaction.Commit();

                    result.code = ResponseCode.ok;
                }catch(Exception ex)
                {
                    Transaction.Rollback();

                    result.code = ResponseCode.error;
                }
                return result;
            }
        }
        public ResponseResult DeleteBrand(int brandId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var brand = (from a in context.brand
                                 where a.isEnable
                                 && a.brandId == brandId
                                 select a).FirstOrDefault();
                    brand.isEnable = false;
                    brand.updateDate = DateTime.Now;
                    brand.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                    context.SaveChanges();
                    Transaction.Commit();

                    result.code = ResponseCode.ok;
                }catch(Exception ex)
                {
                    Transaction.Rollback();

                    result.code = ResponseCode.error;
                }
                return result;
            }
        }
        public bool CheckBrandName(int brandId,string brandName)
        {
            return !(from a in context.brand
                     where a.isEnable
                     && a.brandId != brandId
                     && a.brandName == brandName
                     select a).Any();
        }
        #endregion
        #region for generation
        public List<GenerationViewModel> GetDatatableGeneration(GenerationFilter filter)
        {
            return (from a in context.generation
                    where a.isEnable
                    && a.brandId == filter.brandId
                    && (a.generationName.Contains(filter.generationName) || filter.generationName == null)
                    select new GenerationViewModel
                    {
                        generationId = a.generationId,
                        brandId = a.brandId,
                        generationName = a.generationName,
                        carInGeneration = 0
                    }).ToList();
        }
        public GenerationViewModel GetGenerationById(int brandId,int generationId)
        {
            return (from a in context.generation
                    join b in context.brand on a.brandId equals b.brandId
                    where a.isEnable
                    && a.brandId == brandId
                    && a.generationId == generationId
                    && b.isEnable
                    select new GenerationViewModel
                    {
                        generationId = a.generationId,
                        brandId = a.brandId,
                        generationName = a.generationName,
                        brandName = b.brandName
                    }).FirstOrDefault() ?? new GenerationViewModel();
        }
        public ResponseResult CreateGeneration(GenerationViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.generationId == 0)
                    {
                        generation generation = new generation
                        {
                            brandId = data.brandId,
                            generationName = data.generationName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.generation.Add(generation);
                        context.SaveChanges();
                    }
                    else
                    {
                        var generation = (from a in context.generation
                                          where a.isEnable
                                          && a.brandId == data.brandId
                                          && a.generationId == data.generationId
                                          select a).FirstOrDefault();
                        generation.brandId = data.brandId;
                        generation.generationName = data.generationName;
                        generation.updateDate = DateTime.Now;
                        generation.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        generation.isEnable = true;
                        context.SaveChanges();
                    }
                    Transaction.Commit();

                    result.code = ResponseCode.ok;
                }catch(Exception ex)
                {
                    Transaction.Rollback();

                    result.code = ResponseCode.error;
                }
                return result;
            }
        }
        public ResponseResult DeleteGeneration(int brandId,int generationId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var generation = (from a in context.generation
                                      where a.isEnable
                                      && a.brandId == brandId
                                      && a.generationId == generationId
                                      select a).FirstOrDefault();
                    generation.isEnable = false;
                    generation.updateDate = DateTime.Now;
                    generation.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                    context.SaveChanges();
                    Transaction.Commit();

                    result.code = ResponseCode.ok;
                }catch(Exception ex)
                {
                    Transaction.Rollback();

                    result.code = ResponseCode.error;
                }
                return result;
            }
        }
        public bool CheckGenerationName(int brandId,int generationId,string generationName)
        {
            return !(from a in context.generation
                    where a.isEnable
                    && a.brandId == brandId
                    && a.generationId != generationId
                    && a.generationName == generationName
                    select a).Any();
        }
        #endregion
        #region for face
        public List<FaceViewModel> GetDatatableFace(FaceFilter filter)
        {
            return (from a in context.face
                    where a.isEnable
                    && a.brandId == filter.brandId
                    && a.generationId == filter.generationId
                    && (a.faceName.Contains(filter.faceName) || filter.faceName == null)
                    select new FaceViewModel
                    {
                        faceId = a.faceId,
                        brandId = a.brandId,
                        generationId = a.generationId,
                        faceName = a.faceName,
                        carInFace = 0
                    }).ToList();
        }
        public FaceViewModel GetFaceById(int brandId,int generationId,int faceId)
        {
            return (from a in context.face
                    join b in context.brand on a.brandId equals b.brandId
                    join c in context.generation on a.generationId equals c.generationId
                    where a.isEnable
                    && a.brandId == brandId
                    && a.generationId == generationId
                    && a.faceId == faceId
                    && b.isEnable
                    && c.isEnable
                    select new FaceViewModel
                    {
                        faceId = a.faceId,
                        brandId = a.brandId,
                        brandName = b.brandName,
                        generationId = a.generationId,
                        generationName = c.generationName,
                        faceName = a.faceName
                    }).FirstOrDefault() ?? new FaceViewModel();
        }
        public ResponseResult CreateFace(FaceViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.faceId == 0)
                    {
                        face face = new face
                        {
                            brandId = data.brandId,
                            generationId = data.generationId,
                            faceName = data.faceName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.face.Add(face);
                        context.SaveChanges();
                    }
                    else
                    {
                        var face = (from a in context.face
                                    where a.isEnable
                                    && a.brandId == data.brandId
                                    && a.generationId == data.generationId
                                    && a.faceId == data.faceId
                                    select a).FirstOrDefault();
                        face.faceName = data.faceName;
                        face.updateDate = DateTime.Now;
                        face.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        face.isEnable = true;
                        context.SaveChanges();
                    }
                    Transaction.Commit();

                    result.code = ResponseCode.ok;
                }catch(Exception ex)
                {
                    Transaction.Rollback();

                    result.code = ResponseCode.error;
                }
                return result;
            }
        }
        public ResponseResult DeleteFace(int brandId,int generationId,int faceId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var face = (from a in context.face
                                where a.isEnable
                                && a.brandId == brandId
                                && a.generationId == generationId
                                && a.faceId == faceId
                                select a).FirstOrDefault();
                    face.isEnable = false;
                    face.updateDate = DateTime.Now;
                    face.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                    context.SaveChanges();
                    Transaction.Commit();

                    result.code = ResponseCode.ok;
                }catch(Exception ex)
                {
                    Transaction.Rollback();

                    result.code = ResponseCode.error;
                }
                return result;
            }
        }
        public bool CheckFaceName(int brandId,int generationId,int faceId,string faceName)
        {
            return !(from a in context.face
                     where a.isEnable
                     && a.brandId == brandId
                     && a.generationId == generationId
                     && a.faceId != faceId
                     && a.faceName == faceName
                     select a).Any();
        }
        #endregion
        #region for subface
        public List<SubFaceViewModel> GetDatatableSubface(SubFaceFilter filter)
        {
            return (from a in context.subface
                    where a.isEnable
                    && a.brandId == filter.brandId
                    && a.generationId == filter.generationId
                    && a.faceId == filter.faceId
                    && (a.subfaceName.Contains(filter.subfaceName) || filter.subfaceName == null)
                    select new SubFaceViewModel
                    {
                        subfaceId = a.subfaceId,
                        brandId = a.brandId,
                        generationId = a.generationId,
                        faceId = a.faceId,
                        subfaceName = a.subfaceName,
                        carInSubface = 0
                    }).ToList();
        }
        public SubFaceViewModel GetSubfaceById(int brandId,int generationId,int faceId,int subfaceId)
        {
            return (from a in context.subface
                    where a.isEnable
                    && a.brandId == brandId
                    && a.generationId == generationId
                    && a.faceId == faceId
                    && a.subfaceId == subfaceId
                    select new SubFaceViewModel
                    {
                        subfaceId = a.subfaceId,
                        brandId = a.brandId,
                        generationId = a.generationId,
                        faceId = a.faceId,
                        subfaceName = a.subfaceName
                    }).FirstOrDefault() ?? new SubFaceViewModel();
        }
        public ResponseResult CreateSubface(SubFaceViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.subfaceId == 0)
                    {
                        subface subface = new subface
                        {
                            brandId = data.brandId,
                            generationId = data.generationId,
                            faceId = data.faceId,
                            subfaceName = data.subfaceName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.subface.Add(subface);
                        context.SaveChanges();
                    }
                    else
                    {
                        var subface = (from a in context.subface
                                       where a.isEnable
                                       && a.brandId == data.brandId
                                       && a.generationId == data.generationId
                                       && a.faceId == data.faceId
                                       && a.subfaceId == data.subfaceId
                                       select a).FirstOrDefault();
                        subface.brandId = data.brandId;
                        subface.generationId = data.generationId;
                        subface.faceId = data.faceId;
                        subface.subfaceName = data.subfaceName;
                        subface.updateDate = DateTime.Now;
                        subface.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        subface.isEnable = true;
                        context.SaveChanges();
                    }
                    Transaction.Commit();

                    result.code = ResponseCode.ok;
                }catch(Exception ex)
                {
                    Transaction.Rollback();

                    result.code = ResponseCode.error;
                }
                return result;
            }
        }
        public ResponseResult DeleteSubface(int brandId,int generationId,int faceId,int subfaceId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var subface = (from a in context.subface
                                   where a.isEnable
                                   && a.brandId == brandId
                                   && a.generationId == generationId
                                   && a.faceId == faceId
                                   && a.subfaceId == subfaceId
                                   select a).FirstOrDefault();
                    subface.isEnable = false;
                    subface.updateDate = DateTime.Now;
                    subface.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                    context.SaveChanges();
                    Transaction.Commit();

                    result.code = ResponseCode.ok;
                }catch(Exception ex)
                {
                    Transaction.Rollback();

                    result.code = ResponseCode.error;
                }
                return result;
            }
        }
        public bool CheckSubfaceName(int brandId,int generationId,int faceId,int subfaceId,string subfaceName)
        {
            return !(from a in context.subface
                    where a.isEnable
                    && a.brandId == brandId
                    && a.generationId == generationId
                    && a.faceId == faceId
                    && a.subfaceId != subfaceId
                    && a.subfaceName == subfaceName
                    select a).Any();
        }
        #endregion
    }
}
