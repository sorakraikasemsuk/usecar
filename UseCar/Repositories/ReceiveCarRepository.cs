﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
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
        readonly FileManagement file;
        readonly IConfiguration configuration;
        public ReceiveCarRepository(UseCarDBContext context, IHttpContextAccessor httpContext, FileManagement file, IConfiguration configuration)
        {
            this.context = context;
            this.httpContext = httpContext.HttpContext;
            this.file = file;
            this.configuration = configuration;
        }
        public IEnumerable<ReceiveCarDatatableViewModel> GetDatatable()
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("UseCarDBContext")))
            {
                var data = connection.Query<ReceiveCarDatatableViewModel>("st_getReceiveCarList", commandType: CommandType.StoredProcedure);
                return data;
            }
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
                            receiveDate = DateTime.ParseExact(data.receiveDateHidden,"yyyy-MM-dd",CultureInfo.InvariantCulture),
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
                            registerDate = DateTime.ParseExact(data.registerDateHidden,"yyyy-MM-dd",CultureInfo.InvariantCulture),
                            registerNumber = data.registerNumber,
                            provinceId = data.provinceId,
                            createDate = DateTime.Now,
                            createUser = Convert.ToInt32(httpContext.Session.GetString(Session.userId)),
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
                            ownerDate = DateTime.ParseExact(data.ownerDateHidden, "yyyy-MM-dd", CultureInfo.InvariantCulture),
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
        public ReceiveCarViewModel View(int carId)
        {
            var registerData = (from a in context.car_register
                                join b in context.car_owner on a.registerId equals b.registerId
                                where a.isEnable
                                && a.carId == carId
                                && b.isEnable
                                select new {
                                    a.registerDate,
                                    a.registerNumber,
                                    a.provinceId,
                                    b.order,
                                    b.ownerDate,
                                    b.ownerName,
                                    b.ownerAddress
                                }).FirstOrDefault();   
            return (from a in context.car
                    join b in context.brand on a.branchId equals b.brandId
                    join c in context.generation on a.generationId equals c.generationId
                    join d in context.face on a.faceId equals d.faceId
                    join e in context.subface on a.subfaceId equals e.subfaceId
                    join f in context.nature on a.natureId equals f.natureId
                    where a.isEnable
                    && a.carId == carId
                    && b.isEnable
                    && c.isEnable
                    && d.isEnable
                    && e.isEnable
                    && f.isEnable
                    select new ReceiveCarViewModel
                    {
                        carId = a.carId,
                        code = a.code,
                        branchId = a.branchId,
                        categoryId = a.categoryId,
                        brandId = a.brandId,
                        brandName = b.brandName,
                        generationId = a.generationId,
                        generationName = c.generationName,
                        faceId = a.faceId,
                        faceName = d.faceName,
                        subfaceId = a.subfaceId,
                        subfaceName = e.subfaceName,
                        serialNumber = a.serialNumber,
                        engineNumber = a.engineNumber,
                        mileNumber = a.mileNumber,
                        brandEngine = a.brandEngine,
                        gasNumber = a.gasNumber,
                        weight = a.weight,
                        colorId = a.colorId,
                        gearId = a.gearId,
                        seatId = a.seatId,
                        driveSystemId = a.driveSystemId,
                        engineTypeId = a.engineTypeId,
                        capacityEngineId = a.capacityEngineId,
                        typeId = a.typeId,
                        natureId = a.natureId,
                        natureName = f.natureName,
                        year = a.year,
                        receiveDateHidden = a.receiveDate.ToString("yyyy-MM-dd"),
                        receiveCarStatusId = a.receiveCarStatusId,
                        vendorId = a.vendorId,
                        vendorName = a.vendorName,
                        vendorAddress = a.vendorAddress,
                        vendorTel = a.vendorTel,
                        vendorNumber = a.vendorNumber,
                        remark = a.remark,
                        buyPrice = a.buyPrice,
                        options = (from option in context.car_option
                                   where option.carId == a.carId
                                   select new ReceiveCarOption
                                   {
                                       optionId = option.optionId
                                   }).ToList(),
                        registerDateHidden = registerData.registerDate.ToString("yyyy-MM-dd"),
                        registerNumber = registerData.registerNumber,
                        provinceId = registerData.provinceId,
                        order = registerData.order,
                        ownerDateHidden = registerData.ownerDate.ToString("yyyy-MM-dd"),
                        ownerName = registerData.ownerName,
                        ownerAddress = registerData.ownerAddress,
                        imageDisplay = (from image in context.car_image
                                        where image.isEnable
                                        && image.carId == a.carId
                                        && image.menuId == MenuId.ReceiveCar
                                        select new ImageDisplay
                                        {
                                            name = image.name,
                                            image = GetImage($"{configuration["Upload:Path"]}{a.code}\\{MenuName.ReceiveCar}\\{image.name}")
                                        }
                                     ).ToList()
                    }).FirstOrDefault();
        }
        public string GetImage(string pathImage)
        {
            using (var stream = new FileStream(pathImage, FileMode.Open))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }
}
