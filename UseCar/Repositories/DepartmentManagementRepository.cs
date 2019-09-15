using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Helper;
using UseCar.Models;
using UseCar.ViewModels;

namespace UseCar.Repositories
{
    public class DepartmentManagementRepository
    {
        readonly UseCarDBContext context;
        public DepartmentManagementRepository(UseCarDBContext context)
        {
            this.context = context;
        }
        public List<DepartmentManagementViewModel> GetDepartmentSearchResult(DepartmentManagementFilter filter)
        {
            return (from a in context.department
                    where a.isEnable
                    && (a.departmentName.Contains(filter.departmentName) || filter.departmentName == null)
                    select new DepartmentManagementViewModel
                    {
                        departmentId = a.departmentId,
                        departmentName = a.departmentName,
                        updateDate = a.updateDate == null ? a.createDate : Convert.ToDateTime(a.updateDate)
                    }).ToList();
        }
        public DepartmentManagementViewModel GetDepartmentById(int departmentId)
        {
            DepartmentManagementViewModel viewModel = new DepartmentManagementViewModel();
            var department = (from a in context.department
                        where a.isEnable && a.departmentId == departmentId
                        select new {
                            a.departmentId,
                            a.departmentName
                        }).FirstOrDefault();
            if (department != null)
            {
                viewModel.departmentId = department.departmentId;
                viewModel.departmentName = department.departmentName;
            }
            return viewModel;
        }
        public ResponseResult Create(DepartmentManagementViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.departmentId == 0)
                    {
                        department depart = new department
                        {
                            departmentName = data.departmentName,
                            createDate = DateTime.Now,
                            createUser = 1,
                            isEnable = true
                        };
                        context.department.Add(depart);
                        context.SaveChanges();
                    }
                    else
                    {
                        var department = (from a in context.department
                                          where a.isEnable && a.departmentId == data.departmentId
                                          select a).FirstOrDefault();
                        department.departmentName = data.departmentName;
                        department.updateDate = DateTime.Now;
                        department.updateUser = 1;
                        department.isEnable = true;
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
        public ResponseResult Delete(int departmentId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var delDepart = (from a in context.department
                                     where a.isEnable && a.departmentId == departmentId
                                     select a).FirstOrDefault();
                    delDepart.isEnable = false;
                    delDepart.updateDate = DateTime.Now;
                    delDepart.updateUser = 1;
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
    }
}
