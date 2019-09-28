using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.Helper
{
    public class FileManagement
    {
        readonly IConfiguration configuration;
        public FileManagement(IConfiguration configuration)
        {
            this.configuration = configuration;
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
    }
}
