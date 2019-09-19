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
        #region for gear
        public List<GearViewModel> GetDatatableGear(GearFilter filter)
        {
            return (from a in context.gear
                    where a.isEnable
                    && (a.gearName.Contains(filter.gearName) || filter.gearName == null)
                    select new GearViewModel
                    {
                        gearId = a.gearId,
                        gearName = a.gearName,
                        carInGear = 0
                    }).ToList();
        }
        public GearViewModel GetGearById(int gearId)
        {
            return (from a in context.gear
                    where a.isEnable
                    && a.gearId == gearId
                    select new GearViewModel
                    {
                        gearId = a.gearId,
                        gearName = a.gearName
                    }).FirstOrDefault() ?? new GearViewModel();
        }
        public ResponseResult CreateGear(GearViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.gearId == 0)
                    {
                        gear gear = new gear
                        {
                            gearName = data.gearName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.gear.Add(gear);
                        context.SaveChanges();
                    }
                    else
                    {
                        var gear = (from a in context.gear
                                    where a.isEnable
                                    && a.gearId == data.gearId
                                    select a).FirstOrDefault();
                        gear.gearName = data.gearName;
                        gear.updateDate = DateTime.Now;
                        gear.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        gear.isEnable = true;
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
        public ResponseResult DeleteGear(int gearId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var gear = (from a in context.gear
                                where a.isEnable
                                && a.gearId == gearId
                                select a).FirstOrDefault();
                    gear.isEnable = false;
                    gear.updateDate = DateTime.Now;
                    gear.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckGearName(int gearId,string gearName)
        {
            return !(from a in context.gear
                     where a.isEnable
                     && a.gearId != gearId
                     && a.gearName == gearName
                     select a).Any();
        }
        #endregion
        #region for capacityEngine
        public List<CapacityEngineViewModel> GetDatatableCapacityEngine(CapacityEngineFilter filter)
        {
            return (from a in context.capacityengine
                    where a.isEnable
                    && (a.capacityEngineName.Contains(filter.capacityEngineName) || filter.capacityEngineName == null)
                    select new CapacityEngineViewModel
                    {
                        capacityEngineId = a.capacityEngineId,
                        capacityEngineName = a.capacityEngineName,
                        carInCapacityEngine = 0
                    }).ToList();
        }
        public CapacityEngineViewModel GetCapacityEngineById(int capacityEngineId)
        {
            return (from a in context.capacityengine
                    where a.isEnable
                    && a.capacityEngineId == capacityEngineId
                    select new CapacityEngineViewModel
                    {
                        capacityEngineId = a.capacityEngineId,
                        capacityEngineName = a.capacityEngineName
                    }).FirstOrDefault() ?? new CapacityEngineViewModel();
        }
        public ResponseResult CreateCapacityEngine(CapacityEngineViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.capacityEngineId == 0)
                    {
                        capacityengine capacityengine = new capacityengine
                        {
                            capacityEngineName = data.capacityEngineName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.capacityengine.Add(capacityengine);
                        context.SaveChanges();
                    }
                    else
                    {
                        var capacityEngine = (from a in context.capacityengine
                                              where a.isEnable
                                              && a.capacityEngineId == data.capacityEngineId
                                              select a).FirstOrDefault();
                        capacityEngine.capacityEngineName = data.capacityEngineName;
                        capacityEngine.updateDate = DateTime.Now;
                        capacityEngine.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        capacityEngine.isEnable = true;
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
        public ResponseResult DeleteCapacityEngine(int capacityEngineId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var capacityEngine = (from a in context.capacityengine
                                          where a.isEnable
                                          && a.capacityEngineId == capacityEngineId
                                          select a).FirstOrDefault();
                    capacityEngine.isEnable = false;
                    capacityEngine.updateDate = DateTime.Now;
                    capacityEngine.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckCapacityEngineName(int capacityEngineId,string capacityEngineName)
        {
            return !(from a in context.capacityengine
                     where a.isEnable
                     && a.capacityEngineId != capacityEngineId
                     && a.capacityEngineName == capacityEngineName
                     select a).Any();
        }
        #endregion
        #region for category
        public List<CategoryViewModel> GetDatatableCategory(CategoryFilter filter)
        {
            return (from a in context.category
                    where a.isEnable
                    && (a.categoryName.Contains(filter.categoryName) || filter.categoryName == null)
                    select new CategoryViewModel
                    {
                        categoryId = a.categoryId,
                        categoryName = a.categoryName,
                        carInCate = 0
                    }).ToList();
        }
        public CategoryViewModel GetCategoryById(int categoryId)
        {
            return (from a in context.category
                    where a.isEnable
                    && a.categoryId == categoryId
                    select new CategoryViewModel
                    {
                        categoryId = a.categoryId,
                        categoryName = a.categoryName
                    }).FirstOrDefault() ?? new CategoryViewModel();
        }
        public ResponseResult CreateCategory(CategoryViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.categoryId == 0)
                    {
                        category category = new category
                        {
                            categoryName = data.categoryName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.category.Add(category);
                        context.SaveChanges();
                    }
                    else
                    {
                        var category = (from a in context.category
                                        where a.isEnable
                                        && a.categoryId == data.categoryId
                                        select a).FirstOrDefault();
                        category.categoryName = data.categoryName;
                        category.updateDate = DateTime.Now;
                        category.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        category.isEnable = true;
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
        public ResponseResult DeleteCategory(int categoryId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var category = (from a in context.category
                                    where a.isEnable
                                    && a.categoryId == categoryId
                                    select a).FirstOrDefault();
                    category.isEnable = false;
                    category.updateDate = DateTime.Now;
                    category.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckCategoryName(int categoryId,string categoryName)
        {
            return !(from a in context.category
                     where a.isEnable
                     && a.categoryId != categoryId
                     && a.categoryName == categoryName
                     select a).Any();
        }
        #endregion
        #region for seat
        public List<SeatViewModel> GetDatatableSeat(SeatFilter filter)
        {
            return (from a in context.seat
                    where a.isEnable
                    && (a.seatName.Contains(filter.seatName) || filter.seatName == null)
                    select new SeatViewModel
                    {
                        seatId = a.seatId,
                        seatName = a.seatName,
                        carInSeat = 0
                    }).ToList();
        }
        public SeatViewModel GetSeatById(int seatId)
        {
            return (from a in context.seat
                    where a.isEnable
                    && a.seatId == seatId
                    select new SeatViewModel
                    {
                        seatId = a.seatId,
                        seatName = a.seatName
                    }).FirstOrDefault() ?? new SeatViewModel();
        }
        public ResponseResult CreateSeat(SeatViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.seatId == 0)
                    {
                        seat seat = new seat
                        {
                            seatName = data.seatName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.seat.Add(seat);
                        context.SaveChanges();
                    }
                    else
                    {
                        var seat = (from a in context.seat
                                    where a.isEnable
                                    && a.seatId == data.seatId
                                    select a).FirstOrDefault();
                        seat.seatName = data.seatName;
                        seat.updateDate = DateTime.Now;
                        seat.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        seat.isEnable = true;
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
        public ResponseResult DeleteSeat(int seatId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var seat = (from a in context.seat
                                where a.isEnable
                                && a.seatId == seatId
                                select a).FirstOrDefault();
                    seat.isEnable = false;
                    seat.updateDate = DateTime.Now;
                    seat.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckSeatName(int seatId,string seatName)
        {
            return !(from a in context.seat
                     where a.isEnable
                     && a.seatId != seatId
                     && a.seatName == seatName
                     select a).Any();
        }
        #endregion
        #region for option
        public List<OptionViewModel> GetDatatableOption(OptionFilter filter)
        {
            return (from a in context.option
                    where a.isEnable
                    && (a.optionName.Contains(filter.optionName) || filter.optionName == null)
                    select new OptionViewModel
                    {
                        optionId = a.optionId,
                        optionName = a.optionName,
                        carInOption = 0
                    }).ToList();
        }
        public OptionViewModel GetOptionById(int optionId)
        {
            return (from a in context.option
                    where a.isEnable
                    && a.optionId == optionId
                    select new OptionViewModel
                    {
                        optionId = a.optionId,
                        optionName = a.optionName
                    }).FirstOrDefault() ?? new OptionViewModel();
        }
        public ResponseResult CreateOption(OptionViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.optionId == 0)
                    {
                        option option = new option
                        {
                            optionName = data.optionName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.option.Add(option);
                        context.SaveChanges();
                    }
                    else
                    {
                        var option = (from a in context.option
                                      where a.isEnable
                                      && a.optionId == data.optionId
                                      select a).FirstOrDefault();
                        option.optionName = data.optionName;
                        option.updateDate = DateTime.Now;
                        option.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        option.isEnable = true;
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
        public ResponseResult DeleteOption(int optionId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var option = (from a in context.option
                                  where a.isEnable
                                  && a.optionId == optionId
                                  select a).FirstOrDefault();
                    option.isEnable = false;
                    option.updateDate = DateTime.Now;
                    option.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckOptionName(int optionId,string optionName)
        {
            return !(from a in context.option
                     where a.isEnable
                     && a.optionId != optionId
                     && a.optionName == optionName
                     select a).Any();
        }
        #endregion
        #region for driveSystem
        public List<DriveSystemViewModel> GetDatatableDriveSystem(DriveSystemFilter filter)
        {
            return (from a in context.drivesystem
                    where a.isEnable
                    && (a.driveSystemName.Contains(filter.driveSystemName) || filter.driveSystemName == null)
                    select new DriveSystemViewModel
                    {
                        driveSystemId = a.driveSystemId,
                        driveSystemName = a.driveSystemName,
                        carInDrive = 0
                    }).ToList();
        }
        public DriveSystemViewModel GetDriveSystemById(int driveSystemId)
        {
            return (from a in context.drivesystem
                    where a.isEnable
                    && a.driveSystemId == driveSystemId
                    select new DriveSystemViewModel
                    {
                        driveSystemId = a.driveSystemId,
                        driveSystemName = a.driveSystemName
                    }).FirstOrDefault() ?? new DriveSystemViewModel();
        }
        public ResponseResult CreateDriveSystem(DriveSystemViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.driveSystemId == 0)
                    {
                        drivesystem drivesystem = new drivesystem
                        {
                            driveSystemName = data.driveSystemName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.drivesystem.Add(drivesystem);
                        context.SaveChanges();
                    }
                    else
                    {
                        var drivesystem = (from a in context.drivesystem
                                           where a.isEnable
                                           && a.driveSystemId == data.driveSystemId
                                           select a).FirstOrDefault();
                        drivesystem.driveSystemName = data.driveSystemName;
                        drivesystem.updateDate = DateTime.Now;
                        drivesystem.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        drivesystem.isEnable = true;
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
        public ResponseResult DeleteDriveSystem(int driveSystemId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var driveSystem = (from a in context.drivesystem
                                       where a.isEnable
                                       && a.driveSystemId == driveSystemId
                                       select a).FirstOrDefault();
                    driveSystem.isEnable = false;
                    driveSystem.updateDate = DateTime.Now;
                    driveSystem.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckDriveSystemName(int driveSystemId,string driveSystemName)
        {
            return !(from a in context.drivesystem
                     where a.isEnable
                     && a.driveSystemId != driveSystemId
                     && a.driveSystemName == driveSystemName
                     select a).Any();
        }
        #endregion
        #region for color
        public List<ColorViewModel> GetDatatableColor(ColorFilter filter)
        {
            return (from a in context.color
                    where a.isEnable
                    && (a.colorName.Contains(filter.colorName) || filter.colorName == null)
                    select new ColorViewModel
                    {
                        colorId = a.colorId,
                        colorName = a.colorName,
                        carInColor = 0
                    }).ToList();
        }
        public ColorViewModel GetColorById(int colorId)
        {
            return (from a in context.color
                    where a.isEnable
                    && a.colorId == colorId
                    select new ColorViewModel
                    {
                        colorId = a.colorId,
                        colorName = a.colorName
                    }).FirstOrDefault() ?? new ColorViewModel();
        }
        public ResponseResult CreateColor(ColorViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.colorId == 0)
                    {
                        color color = new color
                        {
                            colorName = data.colorName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.color.Add(color);
                        context.SaveChanges();
                    }
                    else
                    {
                        var color = (from a in context.color
                                     where a.isEnable
                                     && a.colorId == data.colorId
                                     select a).FirstOrDefault();
                        color.colorName = data.colorName;
                        color.updateDate = DateTime.Now;
                        color.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        color.isEnable = true;
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
        public ResponseResult DeleteColor(int colorId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var color = (from a in context.color
                                 where a.isEnable
                                 && a.colorId == colorId
                                 select a).FirstOrDefault();
                    color.isEnable = false;
                    color.updateDate = DateTime.Now;
                    color.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckColorName(int colorId,string colorName)
        {
            return !(from a in context.color
                     where a.isEnable
                     && a.colorId != colorId
                     && a.colorName == colorName
                     select a).Any();
        }
        #endregion
        #region for engineType
        public List<EngineTypeViewModel> GetDatatableEngineType(EngineTypeFilter filter)
        {
            return (from a in context.enginetype
                    where a.isEnable
                    && (a.engineTypeName.Contains(filter.engineTypeName) || filter.engineTypeName == null)
                    select new EngineTypeViewModel
                    {
                        engineTypeId = a.engineTypeId,
                        engineTypeName = a.engineTypeName,
                        carInEngine = 0
                    }).ToList();
        }
        public EngineTypeViewModel GetEngineTypeById(int engineTypeId)
        {
            return (from a in context.enginetype
                    where a.isEnable
                    && a.engineTypeId == engineTypeId
                    select new EngineTypeViewModel
                    {
                        engineTypeId = a.engineTypeId,
                        engineTypeName = a.engineTypeName
                    }).FirstOrDefault() ?? new EngineTypeViewModel();
        }
        public ResponseResult CreateEngineType(EngineTypeViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.engineTypeId == 0)
                    {
                        enginetype enginetype = new enginetype
                        {
                            engineTypeName = data.engineTypeName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.enginetype.Add(enginetype);
                        context.SaveChanges();
                    }
                    else
                    {
                        var enginetype = (from a in context.enginetype
                                          where a.isEnable
                                          && a.engineTypeId == data.engineTypeId
                                          select a).FirstOrDefault();
                        enginetype.engineTypeName = data.engineTypeName;
                        enginetype.updateDate = DateTime.Now;
                        enginetype.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        enginetype.isEnable = true;
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
        public ResponseResult DeleteEngineType(int engineTypeId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var enginetype = (from a in context.enginetype
                                      where a.isEnable
                                      && a.engineTypeId == engineTypeId
                                      select a).FirstOrDefault();
                    enginetype.isEnable = false;
                    enginetype.updateDate = DateTime.Now;
                    enginetype.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckEngineTypeName(int engineTypeId,string engineTypeName)
        {
            return !(from a in context.enginetype
                     where a.isEnable
                     && a.engineTypeId != engineTypeId
                     && a.engineTypeName == engineTypeName
                     select a).Any();
        }
        #endregion
    }
}
