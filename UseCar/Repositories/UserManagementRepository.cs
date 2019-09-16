using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCar.Helper;
using UseCar.Models;
using UseCar.ViewModels;

namespace UseCar.Repositories
{
    public class UserManagementRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        public UserManagementRepository(UseCarDBContext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
        }
        public List<UserManagementSearchResult> GetDatatable(UserManagementSearchResultFilter filter)
        {
            return (from a in context.user
                    join b in context.department on a.departmentId equals b.departmentId
                    where a.isEnable
                    && b.isEnable
                    && (a.code.Contains(filter.code) || filter.code == null)
                    && (a.firstName.Contains(filter.firstName) || filter.firstName == null)
                    && (a.lastName.Contains(filter.lastName) || filter.lastName == null)
                    && (a.departmentId == filter.departmentId || filter.departmentId == 0)
                    && (
                    (a.isActive && filter.statusId == 1)
                    || (!a.isActive && filter.statusId == 2)
                    || filter.statusId == 0
                    )
                    select new UserManagementSearchResult
                    {
                        userId = a.userId,
                        code = a.code,
                        firstName = a.firstName,
                        lastName = a.lastName,
                        departmentId = a.departmentId,
                        departmentName = b.departmentName,
                        tel = a.tel,
                        email = a.email,
                        isActive = a.isActive,
                        isAdmin = a.isAdmin
                    }).ToList();
        }
        public UserManagementViewModel GetUserById(int userId)
        {
            return (from a in context.user
                    where a.isEnable
                    && !a.isAdmin
                    && a.userId == userId
                    select new UserManagementViewModel
                    {
                        userId = a.userId,
                        code = a.code,
                        firstName = a.firstName,
                        lastName = a.lastName,
                        departmentId = a.departmentId,
                        tel = a.tel,
                        email = a.email,
                        userName = a.userName,
                        password = a.password,
                        confirmPassword = a.password,
                        isActive = a.isActive
                    }).FirstOrDefault();
        }
        public ResponseResult Create(UserManagementViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var salt = GeneratePassword.GetSalt();
                    if (data.userId == 0)
                    {
                        user user = new user
                        {
                            code = GenerateCode(),
                            firstName = data.firstName,
                            lastName = data.lastName,
                            departmentId = data.departmentId,
                            tel = data.tel,
                            email = data.email,
                            userName = data.userName,
                            password = GeneratePassword.PasswordCreate(data.password,salt),
                            salt= Convert.ToBase64String(salt),
                            isActive = data.isActive,
                            isAdmin = false,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.user.Add(user);
                        context.SaveChanges();
                    }
                    else
                    {
                        var user = (from a in context.user
                                    where a.isEnable && !a.isAdmin && a.userId == data.userId
                                    select a).FirstOrDefault();
                        user.firstName = data.firstName;
                        user.lastName = data.lastName;
                        user.departmentId = data.departmentId;
                        user.tel = data.tel;
                        user.email = data.email;
                        user.isActive = data.isActive;
                        user.isAdmin = false;
                        user.updateDate = DateTime.Now;
                        user.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        user.isEnable = true;
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
        public string GenerateCode()
        {
            var count = (from a in context.user
                         where !a.isAdmin
                         select a).Count();
            return "USER-" + (count + 1).ToString().PadLeft(4, '0');
        }
        public ResponseResult Delete(int userId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    var delUser = (from a in context.user
                                   where a.isEnable
                                   && !a.isAdmin
                                   && a.userId == userId
                                   select a).FirstOrDefault();
                    delUser.isEnable = false;
                    delUser.updateDate = DateTime.Now;
                    delUser.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
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
        public bool CheckUsername(int userId, string userName)
        {
            return !(from a in context.user
                    where a.isEnable
                    && a.userId != userId
                    && a.userName == userName
                    select a).Any();
        }
    }
}
