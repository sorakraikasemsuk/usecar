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
        #region for shop
        public List<RepairShopViewModel> GetDatatableRepairShop(RepairShopFilter filter)
        {
            return (from a in context.repairshop
                    join b in context.categoryshop on a.categoryShopId equals b.categoryShopId
                    where a.isEnable
                    && b.isEnable
                    && (a.repairShopName.Contains(filter.repairShopName) || filter.repairShopName == null)
                    && (b.categoryShopId == filter.categoryShopId || filter.categoryShopId == 0)
                    select new RepairShopViewModel
                    {
                        repairShopId = a.repairShopId,
                        repairShopName = a.repairShopName,
                        tel = a.tel,
                        fax = a.fax,
                        categoryShopId = a.categoryShopId,
                        categoryShopName = b.categoryShopName,
                        maInShop = 0
                    }).ToList();
        }
        public RepairShopViewModel GetRepairShopById(int repairShopId)
        {
            return (from a in context.repairshop
                    where a.isEnable
                    && a.repairShopId == repairShopId
                    select new RepairShopViewModel
                    {
                        repairShopId = a.repairShopId,
                        repairShopName = a.repairShopName,
                        repairShopAddress = a.repairShopAddress,
                        tel = a.tel,
                        fax = a.fax ?? "",
                        categoryShopId = a.categoryShopId,
                        contactRepairShops = (from b in context.contactrepairshop
                                              where b.isEnable
                                              && b.repairShopId == a.repairShopId
                                              select new ContactRepairShop
                                              {
                                                  contactRepairShopId = b.contactRepairShopId,
                                                  contactName = b.contactName,
                                                  tel = b.tel,
                                                  remark = b.remark ?? ""
                                              }).ToList()
                    }).FirstOrDefault() ?? new RepairShopViewModel();
        }
        public ResponseResult CreateRepairShop(RepairShopViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.repairShopId == 0)
                    {
                        repairshop repairshop = new repairshop
                        {
                            repairShopName = data.repairShopName,
                            repairShopAddress = data.repairShopAddress,
                            tel = data.tel,
                            fax = data.fax,
                            categoryShopId = data.categoryShopId,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.repairshop.Add(repairshop);
                        context.SaveChanges();

                        if (data.contactRepairShops != null)
                        {
                            foreach (var contact in data.contactRepairShops)
                            {
                                contactrepairshop contactrepairshop = new contactrepairshop
                                {
                                    repairShopId = repairshop.repairShopId,
                                    contactName = contact.contactName,
                                    tel = contact.tel,
                                    remark = contact.remark,
                                    createDate = DateTime.Now,
                                    createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                                    isEnable = true
                                };
                                context.contactrepairshop.Add(contactrepairshop);
                                context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        var repairShop = (from a in context.repairshop
                                          where a.isEnable
                                          && a.repairShopId == data.repairShopId
                                          select a).FirstOrDefault();
                        repairShop.repairShopName = data.repairShopName;
                        repairShop.repairShopAddress = data.repairShopAddress;
                        repairShop.tel = data.tel;
                        repairShop.fax = data.fax;
                        repairShop.categoryShopId = data.categoryShopId;
                        repairShop.updateDate = DateTime.Now;
                        repairShop.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        repairShop.isEnable = true;
                        context.SaveChanges();

                        //remove old contact
                        var oldContact = (from a in context.contactrepairshop
                                          where a.isEnable
                                          && a.repairShopId == data.repairShopId
                                          select a).ToList();
                        foreach(var item in oldContact)
                        {
                            context.contactrepairshop.Remove(item);
                            context.SaveChanges();
                        }

                        //new contact
                        if (data.contactRepairShops != null)
                        {
                            foreach (var contact in data.contactRepairShops)
                            {
                                contactrepairshop contactrepairshop = new contactrepairshop
                                {
                                    repairShopId = data.repairShopId,
                                    contactName = contact.contactName,
                                    tel = contact.tel,
                                    remark = contact.remark,
                                    createDate = DateTime.Now,
                                    createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                                    isEnable = true
                                };
                                context.contactrepairshop.Add(contactrepairshop);
                                context.SaveChanges();
                            }
                        }
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
        public ResponseResult DeleteRepairShop(int repairShopId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var repairshop = (from a in context.repairshop
                                      where a.isEnable
                                      && a.repairShopId == repairShopId
                                      select a).FirstOrDefault();
                    repairshop.isEnable = false;
                    repairshop.updateDate = DateTime.Now;
                    repairshop.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                    context.SaveChanges();

                    var contact = (from a in context.contactrepairshop
                                   where a.isEnable
                                   && a.repairShopId == repairShopId
                                   select a).ToList();
                    foreach(var item in contact)
                    {
                        context.contactrepairshop.Remove(item);
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
        public bool CheckRepairShopName(int repairShopId,string repairShopName)
        {
            return !(from a in context.repairshop
                     where a.isEnable
                     && a.repairShopId != repairShopId
                     && a.repairShopName == repairShopName
                     select a).Any();
        }
        #endregion
    }
}
