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
    public class RepairShopRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        public RepairShopRepository(UseCarDBContext context,IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
        }
        #region for categoryShop
        public List<CategoryShopViewModel> GetDatatableCategoryShop(CategoryShopFilter filter)
        {
            return (from a in context.categoryshop
                    where a.isEnable
                    && (a.categoryShopName.Contains(filter.categoryShopName) || filter.categoryShopName == null)
                    select new CategoryShopViewModel
                    {
                        categoryShopId = a.categoryShopId,
                        categoryShopName = a.categoryShopName,
                        shopInCate = 0
                    }).ToList();
        }
        public CategoryShopViewModel GetCategoryShopById(int categoryShopId)
        {
            return (from a in context.categoryshop
                    where a.isEnable
                    && a.categoryShopId == categoryShopId
                    select new CategoryShopViewModel
                    {
                        categoryShopId = a.categoryShopId,
                        categoryShopName = a.categoryShopName
                    }).FirstOrDefault() ?? new CategoryShopViewModel();
        }
        public ResponseResult CreateCategoryShop(CategoryShopViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.categoryShopId == 0)
                    {
                        categoryshop categoryshop = new categoryshop
                        {
                            categoryShopName = data.categoryShopName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.categoryshop.Add(categoryshop);
                        context.SaveChanges();
                    }
                    else
                    {
                        var categoryshop = (from a in context.categoryshop
                                            where a.isEnable
                                            && a.categoryShopId == data.categoryShopId
                                            select a).FirstOrDefault();
                        categoryshop.categoryShopName = data.categoryShopName;
                        categoryshop.updateDate = DateTime.Now;
                        categoryshop.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        categoryshop.isEnable = true;
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
        public ResponseResult DeleteCategoryShop(int categoryShopId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var categoryshop = (from a in context.categoryshop
                                        where a.isEnable
                                        && a.categoryShopId == categoryShopId
                                        select a).FirstOrDefault();
                    categoryshop.isEnable = false;
                    categoryshop.updateDate = DateTime.Now;
                    categoryshop.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckCategoryShopName(int categoryShopId,string categoryShopName)
        {
            return !(from a in context.categoryshop
                     where a.isEnable
                     && a.categoryShopId != categoryShopId
                     && a.categoryShopName == categoryShopName
                     select a).Any();
        }
        #endregion
    }
}
