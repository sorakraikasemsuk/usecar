using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.Helper
{
    public class File
    {
        public async Task Upload(IFormFile file,string folderName,string moduleName)
        {
            var uniqueFileName = file.FileName;
            var uploads = Path.Combine("C:\\UseCar\\", folderName, moduleName);
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
    }
}
