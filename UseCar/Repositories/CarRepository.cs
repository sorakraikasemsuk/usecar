using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Helper;
using UseCar.Models;
using UseCar.ViewModels;

namespace UseCar.Repositories
{
    public class CarRepository
    {
        readonly UseCarDBContext context;
        readonly IConfiguration configuration;
        readonly FileManagement file;
        public CarRepository(UseCarDBContext context, IConfiguration configuration, FileManagement file) 
        {
            this.context = context;
            this.configuration = configuration;
            this.file = file;
        }
        public List<CarViewModel> GetDatatable(CarFilter filter)
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("UseCarDBContext")))
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@branchId", filter.branchId);
                queryParameters.Add("@brandId", filter.brandId);
                queryParameters.Add("@generationId", filter.generationId);
                queryParameters.Add("@faceId", filter.faceId);
                queryParameters.Add("@subfaceId", filter.subfaceId);
                queryParameters.Add("@carStatusId", filter.carStatusId);
                queryParameters.Add("@registerNumber", string.IsNullOrEmpty(filter.registerNumber) ? "" : filter.registerNumber);
                var data = connection.Query<CarViewModel>("st_getCarList", queryParameters, commandType: CommandType.StoredProcedure);
                return (from a in data
                        select new CarViewModel
                        {
                            carId = a.carId,
                            brandId = a.brandId,
                            brandName = a.brandName,
                            generationId = a.generationId,
                            generationName = a.generationName,
                            faceId = a.faceId,
                            faceName = a.faceName,
                            subfaceId = a.subfaceId,
                            subfaceName = a.subfaceName,
                            carStatusId=a.carStatusId,
                            statusName=a.statusName,
                            fileName = file.GetImage($"{configuration["Upload:Path"]}{a.code}\\{MenuName.CheckupCar}\\{a.fileName}"),
                            registerNumber = a.registerNumber,
                            buyPrice=a.buyPrice,
                            sellPrice=a.sellPrice
                        }).ToList();
            }
        }
        public CarDetailViewModel View(int carId)
        {
            return (from a in context.car
                    join b in context.branch on a.branchId equals b.branchId
                    join c in context.category on a.categoryId equals c.categoryId
                    join d in context.brand on a.brandId equals d.brandId
                    join e in context.generation on a.generationId equals e.generationId
                    join f in context.face on a.faceId equals f.faceId
                    join g in context.subface on a.subfaceId equals g.subfaceId
                    join h in context.color on a.colorId equals h.colorId
                    join i in context.gear on a.gearId equals i.gearId
                    join j in context.seat on a.seatId equals j.seatId
                    join k in context.drivesystem on a.driveSystemId equals k.driveSystemId
                    join l in context.enginetype on a.engineTypeId equals l.engineTypeId
                    join m in context.capacityengine on a.capacityEngineId equals m.capacityEngineId
                    join n in context.type on a.typeId equals n.typeId
                    join o in context.nature on a.natureId equals o.natureId
                    where a.isEnable
                    && a.carId == carId
                    select new CarDetailViewModel
                    {
                        carId = a.carId,
                        code = a.code,
                        branchId = a.branchId,
                        branchName = b.branchName,
                        categoryId = a.categoryId,
                        categoryName = c.categoryName,
                        brandId = a.brandId,
                        brandName = d.brandName,
                        generationId = a.generationId,
                        generationName = e.generationName,
                        faceId = a.faceId,
                        faceName = f.faceName,
                        subfaceId = a.subfaceId,
                        subfaceName = g.subfaceName,
                        serialNumber = a.serialNumber,
                        engineNumber = a.engineNumber,
                        mileNumber = a.mileNumber,
                        brandEngine = a.brandEngine,
                        gasNumber = a.gasNumber,
                        weight = a.weight,
                        colorId = a.colorId,
                        colorName = h.colorName,
                        gearId = a.gearId,
                        gearName = i.gearName,
                        seatId = a.seatId,
                        seatName = j.seatName,
                        driveSystemId = a.driveSystemId,
                        driveSystemName = k.driveSystemName,
                        engineTypeId = a.engineTypeId,
                        engineTypeName = l.engineTypeName,
                        capacityEngineId = a.capacityEngineId,
                        capacityEngineName = m.capacityEngineName,
                        typeId = a.typeId,
                        typeName = n.typeName,
                        natureId = a.natureId,
                        natureName = o.natureName,
                        year = a.year,
                        remark = a.remark,
                        options = (from carOpt in context.car_option
                                   join option in context.option on carOpt.optionId equals option.optionId
                                   where carOpt.carId == a.carId
                                   && option.isEnable
                                   select new CarOption
                                   {
                                       optionId = carOpt.optionId,
                                       optionName = option.optionName
                                   }).ToList(),
                        registers = (from register in context.car_register
                                     join province in context.provinces on register.provinceId equals province.id
                                     where register.isEnable
                                     && register.carId == a.carId
                                     select new CarRegister
                                     {
                                         registerId = register.registerId,
                                         registerDate = register.registerDate,
                                         registerNumber = register.registerNumber,
                                         provinceId = register.provinceId,
                                         provinceName = province.name_th,
                                         owners = (from owner in context.car_owner
                                                   where owner.isEnable
                                                   && owner.registerId == register.registerId
                                                   select new CarOwner
                                                   {
                                                       ownerId = owner.ownerId,
                                                       order = owner.order,
                                                       ownerDate = owner.ownerDate,
                                                       ownerName = owner.ownerName,
                                                       ownerAddress = owner.ownerAddress
                                                   }).ToList()
                                     }).ToList(),
                        images = (from image in context.car_image
                                  where image.isEnable
                                  && image.carId == a.carId
                                  select new CarImage
                                  {
                                      imageId = image.imageId,
                                      name = image.name,
                                      menuId = image.menuId,
                                      image = file.GetImageByMenu(image.menuId, a.code, image.name, image.refId)
                                  }).ToList()
                    }).FirstOrDefault();
        }
    }
}
