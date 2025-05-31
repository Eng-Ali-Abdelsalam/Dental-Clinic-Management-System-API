using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DentalClinic.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DentalClinic.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<FileService> _logger;
        private readonly HashSet<string> _allowedFileTypes = new HashSet<string>
        {
            // Images
            ".jpg", ".jpeg", ".png", ".gif", ".bmp",
            // Documents
            ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
            // Others
            ".txt", ".csv", ".zip"
        };

        public FileService(IWebHostEnvironment environment, ILogger<FileService> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            // Ensure the folder exists
            var uploadsFolderPath = Path.Combine(_environment.ContentRootPath, "Uploads", folderName);
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            // Generate a unique filename to prevent overwriting
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the relative path for storage in the database
            return Path.Combine("Uploads", folderName, fileName);
        }

        public async Task<(byte[] FileContents, string ContentType)> GetFileAsync(string filePath)
        {
            var fullPath = Path.Combine(_environment.ContentRootPath, filePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File not found: {filePath}");

            var fileBytes = await File.ReadAllBytesAsync(fullPath);
            var contentType = GetContentType(Path.GetExtension(filePath));

            return (fileBytes, contentType);
        }

        public Task DeleteFileAsync(string filePath)
        {
            var fullPath = Path.Combine(_environment.ContentRootPath, filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                _logger.LogInformation($"Deleted file: {filePath}");
            }
            else
            {
                _logger.LogWarning($"File not found for deletion: {filePath}");
            }

            return Task.CompletedTask;
        }

        public bool IsValidFileType(IFormFile file)
        {
            if (file == null)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return _allowedFileTypes.Contains(extension);
        }

        public bool IsValidFileSize(IFormFile file, long maxSizeInBytes)
        {
            if (file == null)
                return false;

            return file.Length <= maxSizeInBytes;
        }

        private string GetContentType(string extension)
        {
            switch (extension.ToLowerInvariant())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                case ".pdf":
                    return "application/pdf";
                case ".doc":
                    return "application/msword";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".ppt":
                    return "application/vnd.ms-powerpoint";
                case ".pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".txt":
                    return "text/plain";
                case ".csv":
                    return "text/csv";
                case ".zip":
                    return "application/zip";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
