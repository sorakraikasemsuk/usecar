using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Models;

namespace UseCar.Helper
{
    public class SharedData
    {
        readonly UseCarDBContext context;
        readonly IConfiguration configuration;
        readonly FileManagement file;
        public SharedData(UseCarDBContext context, IConfiguration configuration, FileManagement file)
        {
            this.context = context;
            this.configuration = configuration;
            this.file = file;
        }
        public List<SharedDataOptionViewModel> OptionData()
        {
            return (from a in context.option
                    where a.isEnable
                    select new SharedDataOptionViewModel
                    {
                        optionId = a.optionId,
                        optionName = a.optionName
                    }).ToList();
        }
        public SheardDataVendorViewModel VendorData(int vendorId)
        {
            return (from a in context.vendor
                    where a.isEnable
                    && a.vendorId == vendorId
                    select new SheardDataVendorViewModel
                    {
                        vendorId = a.vendorId,
                        vendorName = a.vendorName,
                        vendorAddress = a.vendorAddress ?? "",
                        vendorTel = a.vendorTel ?? "",
                        vendorNumber = a.vendorNumber ?? ""
                    }).FirstOrDefault() ?? new SheardDataVendorViewModel();
        }
        public List<SearchCarViewModel> CarSearch(SearchCarFilter filter)
        {
            using(var connection=new MySqlConnection(configuration.GetConnectionString("UseCarDBCOntext")))
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@branchId", filter.branchId);
                queryParameters.Add("@brandId", filter.brandId);
                queryParameters.Add("@generationId", filter.generationId);
                queryParameters.Add("@faceId", filter.faceId);
                queryParameters.Add("@subfaceId", filter.subfaceId);
                queryParameters.Add("@registerNumber", string.IsNullOrEmpty(filter.registerNumber) ? "" : filter.registerNumber);
                var data = connection.Query<SearchCarViewModel>("st_getCarSearchList", queryParameters, commandType: CommandType.StoredProcedure);
                return (from a in data
                        select new SearchCarViewModel
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
                            image = file.GetImage($"{configuration["Upload:Path"]}{a.code}\\{MenuName.ReceiveCar}\\{a.image}"),
                            registerNumber = a.registerNumber
                        }).ToList();
            }
        }
    }
    public class SharedDataOptionViewModel
    {
        public int optionId { get; set; }
        public string optionName { get; set; }
    }
    public class SheardDataVendorViewModel
    {
        public int vendorId { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorTel { get; set; }
        public string vendorNumber { get; set; }
    }
    public class SearchCarViewModel
    {
        public int carId { get; set; }
        public string code { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public int generationId { get; set; }
        public string generationName { get; set; }
        public int faceId { get; set; }
        public string faceName { get; set; }
        public int subfaceId { get; set; }
        public string subfaceName { get; set; }
        public string image { get; set; }
        public string registerNumber { get; set; }
    }
    public class SearchCarFilter
    {
        public int branchId { get; set; }
        public int brandId { get; set; }
        public int generationId { get; set; }
        public int faceId { get; set; }
        public int subfaceId { get; set; }
        public string registerNumber { get; set; }
    }
}
