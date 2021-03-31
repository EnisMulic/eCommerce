using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Core.Interfaces
{
    public interface IImageUploadService
    {
        Task<string> UploadAsync(IFormFile formFile);
        Task<List<string>> UploadAsync(List<IFormFile> formFiles);
    }
}
