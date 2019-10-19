using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Helper;
using UseCar.Models;
using UseCar.ViewModels;

namespace UseCar.Repositories
{
    public class MaintenanceCarRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        readonly FileManagement file;
        readonly IConfiguration configuration;
        public MaintenanceCarRepository(UseCarDBContext context,IHttpContextAccessor httpContext, FileManagement file, IConfiguration configuration)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
            this.file = file;
            this.configuration = configuration;
        }
        public async Task<ResponseResult> Create(MaintenanceCarViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    string code = (from a in context.car where a.carId == data.carId select new { a.code }).FirstOrDefault().code;
                    if (data.maintenanceId == 0)
                    {
                        car_maintenance maintenance = new car_maintenance
                        {
                            code = GenerateCodeMA(),
                            carId = data.carId,
                            repairShopId = data.repairShopId,
                            sendDate = DateTime.ParseExact(data.sendDateHidden, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            receiveDate = DateTime.ParseExact(data.receiveDateHidden, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            remark = data.remark,
                            maintenanceStatusId = data.maintenanceStatusId,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.car_maintenance.Add(maintenance);
                        context.SaveChanges();

                        foreach(var item in data.details)
                        {
                            car_maintenance_detail detail = new car_maintenance_detail
                            {
                                maintenanceId = maintenance.maintenanceId,
                                itemNo = item.itemNo,
                                description = item.description,
                                price = item.price,
                                createDate = DateTime.Now,
                                createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                                isEnable = true
                            };
                            context.car_maintenance_detail.Add(detail);
                            context.SaveChanges();
                        }

                        //file
                        if (data.files != null)
                        {
                            foreach (var image in data.files)
                            {
                                car_image car_Image = new car_image
                                {
                                    carId = data.carId,
                                    name = image.FileName,
                                    contenType = image.ContentType,
                                    menuId = MenuId.MaintenanceCar,
                                    createDate = DateTime.Now,
                                    createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                                    isEnable = true
                                };
                                context.car_image.Add(car_Image);
                                context.SaveChanges();
                                //upload
                                await file.Upload(image, code, $"{MenuName.MaintenanceCar}\\{maintenance.code}");
                            }
                        }
                    }
                    else
                    {
                        var maintenance = (from a in context.car_maintenance
                                           where a.isEnable
                                           && a.maintenanceId == data.maintenanceId
                                           select a).FirstOrDefault();
                        maintenance.carId = data.carId;
                        maintenance.repairShopId = data.repairShopId;
                        maintenance.sendDate = DateTime.ParseExact(data.sendDateHidden, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        maintenance.receiveDate = DateTime.ParseExact(data.receiveDateHidden, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        maintenance.remark = data.remark;
                        maintenance.maintenanceStatusId = data.maintenanceStatusId;
                        maintenance.updateDate = DateTime.Now;
                        maintenance.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        maintenance.isEnable = true;
                        context.SaveChanges();

                        //delete old detail
                        var oldDetail = (from a in context.car_maintenance_detail
                                         where a.maintenanceId == data.maintenanceId
                                         select a).ToList();
                        foreach(var del in oldDetail)
                        {
                            context.car_maintenance_detail.Remove(del);
                            context.SaveChanges();
                        }

                        foreach (var item in data.details)
                        {
                            car_maintenance_detail detail = new car_maintenance_detail
                            {
                                maintenanceId = maintenance.maintenanceId,
                                itemNo = item.itemNo,
                                description = item.description,
                                price = item.price,
                                createDate = DateTime.Now,
                                createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                                isEnable = true
                            };
                            context.car_maintenance_detail.Add(detail);
                            context.SaveChanges();
                        }

                        //Remode old file
                        if (data.deleteFile != null)
                        {
                            var delFile = (from a in context.car_image
                                           where a.isEnable
                                           && a.carId == data.carId
                                           && a.menuId == MenuId.MaintenanceCar
                                           && data.deleteFile.Contains(a.imageId)
                                           select a).ToList();
                            foreach (var item in delFile)
                            {
                                context.car_image.Remove(item);
                                context.SaveChanges();
                                file.Delete(code, $"{MenuName.MaintenanceCar}\\{maintenance.code}", item.name);
                            }
                        }
                        //file
                        if (data.files != null)
                        {
                            foreach (var image in data.files)
                            {
                                car_image car_Image = new car_image
                                {
                                    carId = data.carId,
                                    name = image.FileName,
                                    contenType = image.ContentType,
                                    menuId = MenuId.MaintenanceCar,
                                    createDate = DateTime.Now,
                                    createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                                    isEnable = true
                                };
                                context.car_image.Add(car_Image);
                                context.SaveChanges();
                                //upload
                                await file.Upload(image, code, $"{MenuName.MaintenanceCar}\\{maintenance.code}");
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
        public string GenerateCodeMA()
        {
            var count = (from a in context.car_maintenance
                         where a.createDate.Year == DateTime.Now.Year
                         && a.createDate.Month == DateTime.Now.Month
                         select a).Count();
            return $"MA{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString().PadLeft(2, '0')}-{(count + 1).ToString().PadLeft(4, '0')}";
        }
    }
}
