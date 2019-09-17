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
                    where a.isEnable
                    && a.brandId == brandId
                    && a.generationId == generationId
                    select new GenerationViewModel
                    {
                        generationId = a.generationId,
                        brandId = a.brandId,
                        generationName = a.generationName
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
    }
}
