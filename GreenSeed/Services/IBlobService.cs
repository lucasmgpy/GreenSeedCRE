using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GreenSeed.Services
{
    public interface IBlobService
    {
        Task<string> UploadFileBlobAsync(IFormFile file);
        Task<string> GetBlobUrlAsync(string fileName);
    }
}
