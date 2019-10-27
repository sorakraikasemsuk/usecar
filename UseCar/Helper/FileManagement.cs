using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Models;

namespace UseCar.Helper
{
    public class FileManagement
    {
        readonly IConfiguration configuration;
        readonly UseCarDBContext context;
        public FileManagement(IConfiguration configuration, UseCarDBContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }
        public async Task Upload(IFormFile file,string folderName,string moduleName)
        {
            var uniqueFileName = file.FileName;
            var uploads = Path.Combine(configuration["Upload:Path"], folderName, moduleName);
            var filePath = Path.Combine(uploads, uniqueFileName);

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            if (file.Length > 0)
            {
                using(var stream=new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }
        public void Delete(string folderName,string moduleName,string fileName)
        {
            var uniqueFileName = fileName;
            var uploads = Path.Combine(configuration["Upload:Path"], folderName, moduleName);
            var filePath = Path.Combine(uploads, uniqueFileName);
            if (File.Exists(filePath))
            {
                // If file found, delete it    
                File.Delete(filePath);
            }
        }
        public string GetImage(string pathImage)
        {
            if (File.Exists(pathImage))
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
            return "";
            
        }
        public string GetImageByMenu(int menuId,string code,string fileName,int refId)
        {
            string pathImage = "";
            switch (menuId)
            {
                case MenuId.ReceiveCar:
                    pathImage = $"{configuration["Upload:Path"]}{code}\\{MenuName.ReceiveCar}\\{fileName}";
                    break;
                case MenuId.CheckupCar:
                    pathImage = $"{configuration["Upload:Path"]}{code}\\{MenuName.CheckupCar}\\{fileName}";
                    break;
                case MenuId.MaintenanceCar:
                    string maCode = context.car_maintenance.FirstOrDefault(a => a.maintenanceId == refId).code;
                    pathImage = $"{configuration["Upload:Path"]}{code}\\{MenuName.MaintenanceCar}\\{maCode}\\{fileName}";
                    break;
            }
            if (File.Exists(pathImage))
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
            return "";

        }
    }
}
