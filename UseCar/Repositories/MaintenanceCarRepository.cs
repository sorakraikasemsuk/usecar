using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        readonly ActionCar actionCar;
        public MaintenanceCarRepository(UseCarDBContext context,IHttpContextAccessor httpContext, FileManagement file, IConfiguration configuration,ActionCar actionCar)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
            this.file = file;
            this.configuration = configuration;
            this.actionCar = actionCar;
        }
        public List<MaintenanceCarDatatableViewModel> GetDatatable(MaintenanceCarDatatableFilter filter)
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("UseCarDBContext")))
            {
                DateTime sendDate = DateTime.ParseExact(filter.sendDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime receiveDate = DateTime.ParseExact(filter.receiveDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@branchId", filter.branchId);
                queryParameters.Add("@brandId", filter.brandId);
                queryParameters.Add("@generationId", filter.generationId);
                queryParameters.Add("@faceId", filter.faceId);
                queryParameters.Add("@subfaceId", filter.subfaceId);
                queryParameters.Add("@maintenanceStatusId", filter.maintenanceStatusId);
                queryParameters.Add("@sendDate", sendDate);
                queryParameters.Add("@receiveDate", receiveDate);
                queryParameters.Add("@registerNumber", string.IsNullOrEmpty(filter.registerNumber) ? "" : filter.registerNumber);
                var data = connection.Query<MaintenanceCarDatatableViewModel>("st_getMaintenanceCarList", queryParameters, commandType: CommandType.StoredProcedure);
                return (from a in data
                        select new MaintenanceCarDatatableViewModel
                        {
                            maintenanceId = a.maintenanceId,
                            code = a.code,
                            carId = a.carId,
                            carCode = a.carCode,
                            brandId = a.brandId,
                            brandName = a.brandName,
                            generationId = a.generationId,
                            generationName = a.generationName,
                            faceId = a.faceId,
                            faceName = a.faceName,
                            subfaceId = a.subfaceId,
                            subfaceName = a.subfaceName,
                            sendDate = a.sendDate,
                            receiveDate = a.receiveDate,
                            maintenanceStatusId = a.maintenanceStatusId,
                            maintenanceStatusName = a.maintenanceStatusName,
                            fileName = file.GetImage($"{configuration["Upload:Path"]}{a.carCode}\\{MenuName.MaintenanceCar}\\{a.code}\\{a.fileName}"),
                            registerNumber = a.registerNumber,
                            sendById = a.sendById,
                            sendByName = a.sendByName
                        }).ToList();
            }
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
                            sendById = data.sendById,
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
                        maintenance.sendById = data.sendById;
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

                    //Update Car Status
                    actionCar.UpdateCarStatus(data.carId, MenuId.MaintenanceCar, data.maintenanceStatusId);
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
        public MaintenanceCarViewModel View(int maintenanceId)
        {
            return (from a in context.car_maintenance
                    join b in context.car on a.carId equals b.carId
                    join c in context.brand on b.brandId equals c.brandId
                    join d in context.generation on b.generationId equals d.generationId
                    join e in context.face on b.faceId equals e.faceId
                    join f in context.subface on b.subfaceId equals f.subfaceId
                    where a.isEnable
                    && a.maintenanceStatusId == MaintenanceCarStatus.Send
                    && a.maintenanceId == maintenanceId
                    && b.isEnable
                    select new MaintenanceCarViewModel
                    {
                        maintenanceId = a.maintenanceId,
                        code = a.code,
                        carId = a.carId,
                        carDetail = "[" + (from register in context.car_register
                                           where register.carId == a.carId
                                           && register.isEnable
                                           orderby register.createDate descending
                                           select new { register.registerNumber }).FirstOrDefault().registerNumber + "] " + c.brandName + " " + d.generationName + " " + e.faceName + " " + f.subfaceName,
                        repairShopId = a.repairShopId,
                        sendDateHidden = a.sendDate.ToString("yyyy-MM-dd"),
                        receiveDateHidden = a.receiveDate.ToString("yyyy-MM-dd"),
                        maintenanceStatusId = a.maintenanceStatusId,
                        sendById = a.sendById,
                        remark = a.remark,
                        details = (from detail in context.car_maintenance_detail
                                   where detail.isEnable
                                   && detail.maintenanceId == maintenanceId
                                   select new MaintenanceDetail
                                   {
                                       maintenanceDetailId = detail.maintenanceDetailId,
                                       itemNo = detail.itemNo,
                                       description = detail.description,
                                       price = detail.price
                                   }).ToList(),
                        imageDisplay = (from image in context.car_image
                                        where image.isEnable
                                        && image.carId == a.carId
                                        && image.menuId == MenuId.MaintenanceCar
                                        select new ImageDisplay
                                        {
                                            imageId = image.imageId,
                                            name = image.name,
                                            image = file.GetImage($"{configuration["Upload:Path"]}{(from car in context.car where car.carId == a.carId select new { car.code }).FirstOrDefault().code}\\{MenuName.MaintenanceCar}\\{a.code}\\{image.name}")
                                        }
                                     ).ToList()
                    }).FirstOrDefault();
        }
    }
}
