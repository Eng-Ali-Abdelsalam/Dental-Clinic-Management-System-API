using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DentalClinic.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
        Task<(byte[] FileContents, string ContentType)> GetFileAsync(string filePath);
        Task DeleteFileAsync(string filePath);
        bool IsValidFileType(IFormFile file);
        bool IsValidFileSize(IFormFile file, long maxSizeInBytes);
    }
}
