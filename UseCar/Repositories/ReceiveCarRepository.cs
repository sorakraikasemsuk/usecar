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
    public class ReceiveCarRepository
    {
        readonly UseCarDBContext context;
        readonly HttpContext httpContext;
        readonly File file;
        public ReceiveCarRepository(UseCarDBContext context, IHttpContextAccessor httpContext, File file)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
            this.file = file;
        }
        public async Task<ResponseResult> Create(ReceiveCarViewModel data)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                ResponseResult result = new ResponseResult();
                try
                {
                    if (data.carId == 0)
                    {
                        car car = new car
                        {
                            code = GenerateCodeCar(),
                            branchId = data.branchId,
                            categoryId = data.categoryId,
                            brandId = data.brandId,
                            generationId = data.generationId,
                            faceId = data.faceId,
                            subfaceId = data.subfaceId,
                            serialNumber = data.serialNumber,
                            engineNumber = data.engineNumber,
                            mileNumber = data.mileNumber,
                            brandEngine = data.brandEngine,
                            gasNumber = data.gasNumber,
                            weight = data.weight,
                            colorId = data.colorId,
                            gearId = data.gearId,
                            seatId = data.seatId,
                            driveSystemId = data.driveSystemId,
                            engineTypeId = data.engineTypeId,
                            capacityEngineId = data.capacityEngineId,
                            typeId = data.typeId,
                            natureId = data.natureId,
                            year = data.year,
                            receiveDate = data.receiveDateHidden,
                            carStatusId = CarStatus.ReceiveCar,
                            carProcessId = data.receiveCarStatusId,
                            receiveCarStatusId = data.receiveCarStatusId,
                            vendorId = data.vendorId,
                            vendorName = data.vendorName,
                            vendorAddress = data.vendorAddress,
                            vendorTel = data.vendorTel,
                            vendorNumber = data.vendorNumber,
                            remark = data.remark,
                            buyPrice = data.buyPrice,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.car.Add(car);
                        context.SaveChanges();

                        //option
                        if (data.options != null)
                        {
                            foreach(var item in data.options)
                            {
                                car_option option = new car_option
                                {
                                    carId = car.carId,
                                    optionId = item.optionId
                                };
                                context.car_option.Add(option);
                                context.SaveChanges();
                            }
                        }

                        //Register
                        car_register register = new car_register
                        {
                            carId = car.carId,
                            registerDate = data.registerDateHidden,
                            registerNumber = data.registerNumber,
                            provinceId = data.provinceId,
                            createDate = DateTime.Now,
                            updateUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.car_register.Add(register);
                        context.SaveChanges();

                        //Owner
                        car_owner owner = new car_owner
                        {
                            carId = car.carId,
                            registerId = register.registerId,
                            order = data.order,
                            ownerDate = data.ownerDateHidden,
                            ownerName = data.ownerName,
                            ownerAddress = data.ownerAddress,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                            isEnable = true
                        };
                        context.car_owner.Add(owner);
                        context.SaveChanges();

                        //file
                        if (data.files != null)
                        {
                            foreach(var image in data.files)
                            {
                                car_image car_Image = new car_image
                                {
                                    carId = car.carId,
                                    name = image.FileName,
                                    contenType = image.ContentType,
                                    menuId = MenuId.ReceiveCar,
                                    createDate = DateTime.Now,
                                    createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
                                    isEnable = true
                                };
                                context.car_image.Add(car_Image);
                                context.SaveChanges();
                                //upload
                                await file.Upload(image, car.code, MenuName.ReceiveCar);
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
        public string GenerateCodeCar()
        {
            var count = (from a in context.car
                         where a.createDate.Year == DateTime.Now.Year
                         && a.createDate.Month == DateTime.Now.Month
                         select a).Count();
            return $"CAR{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString().PadLeft(2, '0')}-{(count+1).ToString().PadLeft(4, '0')}";
        }
    }
}
