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
    public class CheckupCarRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        readonly FileManagement file;
        readonly IConfiguration configuration;
        public CheckupCarRepository(UseCarDBContext context, IHttpContextAccessor httpContext, FileManagement file, IConfiguration configuration)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
            this.file = file;
            this.configuration = configuration;
        }
        public List<CheckupCarDatatableViewModel> GetDatatable(CheckupCarDatatableFilter filter)
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("UseCarDBContext")))
            {
                DateTime checkupDate = DateTime.ParseExact(filter.checkupDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@branchId", filter.branchId);
                queryParameters.Add("@brandId", filter.brandId);
                queryParameters.Add("@generationId", filter.generationId);
                queryParameters.Add("@faceId", filter.faceId);
                queryParameters.Add("@subfaceId", filter.subfaceId);
                queryParameters.Add("@checkupDate", checkupDate);
                queryParameters.Add("@registerNumber", string.IsNullOrEmpty(filter.registerNumber) ? "" : filter.registerNumber);
                var data = connection.Query<CheckupCarDatatableViewModel>("st_getCheckupCarList", queryParameters, commandType: CommandType.StoredProcedure);
                return (from a in data
                        select new CheckupCarDatatableViewModel
                        {
                            carCheckupId=a.carCheckupId,
                            carId = a.carId,
                            checkupDate=a.checkupDate,
                            checkupBy=a.checkupBy,
                            checkupByName=a.checkupByName,
                            brandId = a.brandId,
                            brandName = a.brandName,
                            generationId = a.generationId,
                            generationName = a.generationName,
                            faceId = a.faceId,
                            faceName = a.faceName,
                            subfaceId = a.subfaceId,
                            subfaceName = a.subfaceName,
                            fileName = file.GetImage($"{configuration["Upload:Path"]}{a.code}\\{MenuName.CheckupCar}\\{a.fileName}"),
                            registerNumber = a.registerNumber
                        }).ToList();
            }
        }
        public async Task<ResponseResult> Create(CheckupCarViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    string code = (from a in context.car where a.carId == data.carId select new { a.code }).FirstOrDefault().code;
                    if (data.carCheckupId == 0)
                    {
                        car_checkup car_Checkup = new car_checkup
                        {
                            carId = data.carId,
                            checkupDate = DateTime.ParseExact(data.checkupDateHidden, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            checkupBy = data.checkupBy,
                            remark = data.remark,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.car_checkup.Add(car_Checkup);
                        context.SaveChanges();
                        if (data.checkupDetail != null)
                        {
                            foreach(var item in data.checkupDetail)
                            {
                                car_checkup_detail detail = new car_checkup_detail
                                {
                                    carCheckupId = car_Checkup.carCheckupId,
                                    checkupId = item.checkupId
                                };
                                context.car_checkup_detail.Add(detail);
                                context.SaveChanges();
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
                                    menuId = MenuId.CheckupCar,
                                    createDate = DateTime.Now,
                                    createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                                    isEnable = true
                                };
                                context.car_image.Add(car_Image);
                                context.SaveChanges();
                                //upload
                                await file.Upload(image, code, MenuName.CheckupCar);
                            }
                        }
                    }
                    else
                    {
                        var checkup = (from a in context.car_checkup
                                       where a.isEnable
                                       && a.carCheckupId == data.carCheckupId
                                       select a).FirstOrDefault();
                        checkup.carId = data.carId;
                        checkup.checkupDate = DateTime.ParseExact(data.checkupDateHidden, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        checkup.checkupBy = data.checkupBy;
                        checkup.remark = data.remark;
                        checkup.updateDate = DateTime.Now;
                        checkup.updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId));
                        checkup.isEnable = true;
                        context.SaveChanges();

                        var oldCheckup = (from a in context.car_checkup_detail
                                          where a.carCheckupId == data.carCheckupId
                                          select a).ToList();
                        foreach(var item in oldCheckup)
                        {
                            context.car_checkup_detail.Remove(item);
                            context.SaveChanges();
                        }
                        if (data.checkupDetail != null)
                        {
                            foreach (var item in data.checkupDetail)
                            {
                                car_checkup_detail detail = new car_checkup_detail
                                {
                                    carCheckupId = data.carCheckupId,
                                    checkupId = item.checkupId
                                };
                                context.car_checkup_detail.Add(detail);
                                context.SaveChanges();
                            }
                        }

                        //Remode old file
                        if (data.deleteFile != null)
                        {
                            var delFile = (from a in context.car_image
                                           where a.isEnable
                                           && a.carId == data.carId
                                           && a.menuId == MenuId.CheckupCar
                                           && data.deleteFile.Contains(a.imageId)
                                           select a).ToList();
                            foreach (var item in delFile)
                            {
                                context.car_image.Remove(item);
                                context.SaveChanges();
                                file.Delete(code, MenuName.CheckupCar, item.name);
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
                                    menuId = MenuId.CheckupCar,
                                    createDate = DateTime.Now,
                                    createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                                    isEnable = true
                                };
                                context.car_image.Add(car_Image);
                                context.SaveChanges();
                                //upload
                                await file.Upload(image, code, MenuName.CheckupCar);
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
        public CheckupCarViewModel View(int carCheckupId)
        {
            return (from a in context.car_checkup
                    join b in context.car on a.carId equals b.carId
                    join c in context.brand on b.brandId equals c.brandId
                    join d in context.generation on b.generationId equals d.generationId
                    join e in context.face on b.faceId equals e.faceId
                    join f in context.subface on b.subfaceId equals f.subfaceId
                    where a.isEnable
                    && a.carCheckupId == carCheckupId
                    select new CheckupCarViewModel
                    {
                        carCheckupId = a.carCheckupId,
                        carId = a.carId,
                        carDetail = "[" + (from register in context.car_register
                                           where register.carId == a.carId
                                           && register.isEnable
                                           orderby register.createDate descending
                                           select new { register.registerNumber }).FirstOrDefault().registerNumber + "] " + c.brandName + " " + d.generationName + " " + e.faceName + " " + f.subfaceName,
                        checkupDateHidden = a.checkupDate.ToString("yyyy-MM-dd"),
                        checkupBy = a.checkupBy,
                        remark = a.remark,
                        checkupDetail = (from detail in context.car_checkup_detail
                                         where detail.carCheckupId == a.carCheckupId
                                         select new CheckupCarDetail
                                         {
                                             checkupId = detail.checkupId
                                         }).ToList(),
                        imageDisplay = (from image in context.car_image
                                        where image.isEnable
                                        && image.carId == a.carId
                                        && image.menuId == MenuId.CheckupCar
                                        select new ImageDisplay
                                        {
                                            imageId = image.imageId,
                                            name = image.name,
                                            image = file.GetImage($"{configuration["Upload:Path"]}{(from car in context.car where car.carId == a.carId select new { car.code }).FirstOrDefault().code}\\{MenuName.CheckupCar}\\{image.name}")
                                        }
                                     ).ToList()
                    }).FirstOrDefault();
        }
    }
}
