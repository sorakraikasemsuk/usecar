using Microsoft.AspNetCore.Http;
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
    public class CheckupCarRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        readonly FileManagement file;
        public CheckupCarRepository(UseCarDBContext context, IHttpContextAccessor httpContext, FileManagement file)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
            this.file = file;
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
    }
}
