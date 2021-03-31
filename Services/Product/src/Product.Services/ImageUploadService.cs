using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Product.Core.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Product.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _directory;

        public ImageUploadService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _directory = _hostingEnvironment.ContentRootPath + "\\Upload\\";
            TryCreateDirectory(_directory);
        }

        public async Task<string> UploadAsync(IFormFile formFile)
        {
            using (FileStream fileStream = File.Create(_directory + formFile.FileName))
            {
                await formFile.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return _directory + formFile.FileName;
            }
        }

        public async Task<List<string>> UploadAsync(List<IFormFile> formFiles)
        {
            List<string> images = new();
            foreach(var formFile in formFiles)
            {
                var path = await UploadAsync(formFile);
                images.Add(path);
            }

            return images;
        }

        private void TryCreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
