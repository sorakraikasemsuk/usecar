using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Helper;
using UseCar.Models;
using UseCar.ViewModels;

namespace UseCar.Repositories
{
    public class PermissionManagementRepository
    {
        readonly UseCarDBContext context;
        public PermissionManagementRepository(UseCarDBContext context)
        {
            this.context = context;
        }
        public List<MenuViewModel> GetMenu()
        {
            return (from a in context.m_menu
                    where a.isEnable
                    select new MenuViewModel
                    {
                        menuId = a.menuId,
                        menuName = a.menuName,
                        ord = a.ord
                    }).OrderBy(o=>o.ord).ToList();
        }
        public List<DepartmentMenuPermissionViewModel> GetDepartmentMenuPermission(int departmentId)
        {
            return (from a in context.m_menupermission
                    join b in context.permission on a.menuPermissionId equals b.menuPermissionId into b_join
                    from b in b_join.Where(a => a.departmentId == departmentId).DefaultIfEmpty()
                    where a.isEnable
                    select new DepartmentMenuPermissionViewModel
                    {
                        menuPermissionId = a.menuPermissionId,
                        menuId = a.menuId,
                        permission = a.permission,
                        permissionName = a.permissionName,
                        ord = a.ord,
                        departmentMenuPermissionId = b.menuPermissionId == null ? 0 : b.menuPermissionId
                    }).ToList();
        }
        public ResponseResult Create(DepartmentMenuPermissionParam param)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    //remove old permission
                    var oldPermission = (from a in context.permission
                                         where a.departmentId == param.departmentId
                                         select a).ToList();
                    foreach(var remove in oldPermission)
                    {
                        context.permission.Remove(remove);
                        context.SaveChanges();
                    }
                    //new permission
                    int[] menuPermissionId = Array.ConvertAll(param.menuPermissionId.Split(','), int.Parse);
                    for(int i = 0; i < menuPermissionId.Length; i++)
                    {
                        permission permission = new permission
                        {
                            departmentId = param.departmentId,
                            menuPermissionId = menuPermissionId[i]
                        };
                        context.permission.Add(permission);
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
    }
}
