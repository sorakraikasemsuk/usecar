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
    public class CheckupSettingRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        public CheckupSettingRepository(UseCarDBContext context,IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
        }
        public List<CheckupSettingViewModel> GetDatatable(CheckupFilter filter)
        {
            return (from a in context.checkup
                    where a.isEnable
                    && (a.checkupName.Contains(filter.checkupName) || filter.checkupName == null)
                    select new CheckupSettingViewModel
                    {
                        checkupId = a.checkupId,
                        checkupName = a.checkupName,
                        carInCheck = 0
                    }).ToList();
        }
        public CheckupSettingViewModel GetCheckupById(int checkupId)
        {
            return (from a in context.checkup
                    where a.isEnable
                    && a.checkupId == checkupId
                    select new CheckupSettingViewModel
                    {
                        checkupId = a.checkupId,
                        checkupName = a.checkupName
                    }).FirstOrDefault() ?? new CheckupSettingViewModel();
        }
        public ResponseResult Create(CheckupSettingViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.checkupId == 0)
                    {
                        checkup checkup = new checkup
                        {
                            checkupName = data.checkupName,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.checkup.Add(checkup);
                        context.SaveChanges();
                    }
                    else
                    {
                        var checkup = (from a in context.checkup
                                       where a.isEnable
                                       && a.checkupId == data.checkupId
                                       select a).FirstOrDefault();
                        checkup.checkupName = data.checkupName;
                        checkup.updateDate = DateTime.Now;
                        checkup.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        checkup.isEnable = true;
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
        public ResponseResult Delete(int checkupId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var checkup = (from a in context.checkup
                                   where a.isEnable
                                   && a.checkupId == checkupId
                                   select a).FirstOrDefault();
                    checkup.isEnable = false;
                    checkup.updateDate = DateTime.Now;
                    checkup.updateUser= Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckName(int checkupId,string checkupName)
        {
            return !(from a in context.checkup
                     where a.isEnable
                     && a.checkupId != checkupId
                     && a.checkupName == checkupName
                     select a).Any();
        }
    }
}
