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
    public class VendorRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        public VendorRepository(UseCarDBContext context,IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
        }
        public List<VendorViewModel> GetDatatable(VendorFilter filter)
        {
            return (from a in context.vendor
                    where a.isEnable
                    && (a.vendorName.Contains(filter.vendorName) || filter.vendorName == null)
                    select new VendorViewModel
                    {
                        vendorId = a.vendorId,
                        vendorName = a.vendorName,
                        vendorAddress = a.vendorAddress ?? "",
                        vendorTel = a.vendorTel ?? "",
                        vendorNumber = a.vendorNumber ?? "",
                        carInVendor = 0
                    }).ToList();
        }
        public VendorViewModel GetVendorById(int vendorId)
        {
            return (from a in context.vendor
                    where a.isEnable
                    && a.vendorId == vendorId
                    select new VendorViewModel
                    {
                        vendorId = a.vendorId,
                        vendorName = a.vendorName,
                        vendorAddress = a.vendorAddress ?? "",
                        vendorTel = a.vendorTel ?? "",
                        vendorNumber = a.vendorNumber ?? ""
                    }).FirstOrDefault() ?? new VendorViewModel();
        }
        public ResponseResult Create(VendorViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.vendorId == 0)
                    {
                        vendor vendor = new vendor
                        {
                            vendorName = data.vendorName,
                            vendorAddress = data.vendorAddress,
                            vendorTel = data.vendorTel,
                            vendorNumber = data.vendorNumber,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.vendor.Add(vendor);
                        context.SaveChanges();
                    }
                    else
                    {
                        var vendor = (from a in context.vendor
                                      where a.isEnable
                                      && a.vendorId == data.vendorId
                                      select a).FirstOrDefault();
                        vendor.vendorName = data.vendorName;
                        vendor.vendorAddress = data.vendorAddress;
                        vendor.vendorTel = data.vendorTel;
                        vendor.vendorNumber = data.vendorNumber;
                        vendor.updateDate = DateTime.Now;
                        vendor.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        vendor.isEnable = true;
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
        public ResponseResult Delete(int vendorId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var vendor = (from a in context.vendor
                                  where a.isEnable
                                  && a.vendorId == vendorId
                                  select a).FirstOrDefault();
                    vendor.isEnable = false;
                    vendor.updateDate = DateTime.Now;
                    vendor.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckVendorName(int vendorId,string vendorName)
        {
            return !(from a in context.vendor
                     where a.isEnable
                     && a.vendorId != vendorId
                     && a.vendorName == vendorName
                     select a).Any();
        }
    }
}
