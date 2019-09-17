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
    public class ManageBranchRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        public ManageBranchRepository(UseCarDBContext context,IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
        }
        public List<ManageBranchViewModel> GetDatatable(ManageBranchFilter filter)
        {
            return (from a in context.branch
                    where a.isEnable
                    && (a.branchName.Contains(filter.branchName) || filter.branchName == null)
                    select new ManageBranchViewModel
                    {
                        branchId = a.branchId,
                        branchName = a.branchName,
                        branchAddress = a.branchAddress,
                        tel = a.tel,
                        carInBranch = 0
                    }).ToList();
        }
        public ManageBranchViewModel GetBranchById(int branchId)
        {
            return (from a in context.branch
                    where a.isEnable
                    && a.branchId == branchId
                    select new ManageBranchViewModel {
                        branchId = a.branchId,
                        branchName = a.branchName,
                        branchAddress = a.branchAddress,
                        tel = a.tel,
                    }).FirstOrDefault() ?? new ManageBranchViewModel();
        }
        public ResponseResult Create(ManageBranchViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.branchId == 0)
                    {
                        branch branch = new branch
                        {
                            branchName = data.branchName,
                            branchAddress = data.branchAddress,
                            tel = data.tel,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.branch.Add(branch);
                        context.SaveChanges();
                    }
                    else
                    {
                        var branch = (from a in context.branch
                                      where a.isEnable
                                      && a.branchId == data.branchId
                                      select a).FirstOrDefault();
                        branch.branchName = data.branchName;
                        branch.branchAddress = data.branchAddress;
                        branch.tel = data.tel;
                        branch.updateDate = DateTime.Now;
                        branch.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        branch.isEnable = true;
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
        public ResponseResult Delete(int branchId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var branch = (from a in context.branch
                                  where a.isEnable
                                  && a.branchId == branchId
                                  select a).FirstOrDefault();
                    branch.isEnable = false;
                    branch.updateDate = DateTime.Now;
                    branch.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckBranchName(int branchId,string branchName)
        {
            return !(from a in context.branch
                    where a.isEnable
                    && a.branchId != branchId
                    && a.branchName == branchName
                    select a).Any();
        }
    }
}
