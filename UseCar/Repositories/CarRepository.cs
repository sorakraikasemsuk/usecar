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
    }
}
