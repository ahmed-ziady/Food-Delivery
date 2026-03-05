using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.X509;

namespace FoodDelivery.Infrastructure.Service
{
    public sealed class LocalImageStorageService(IWebHostEnvironment environment) : IImageStorageService
    {
        private const int MaxFileSize = 5 * 1024 * 1024;


        public async Task<string> UploadAsync(IFormFile file,string folderName, CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
                throw new BadRequestException("Image.Empty", "File is empty.");

            if (!file.ContentType.StartsWith("image/"))
                throw new BadRequestException("Image.InvalidType", "Only image files are allowed.");

            if (file.Length > MaxFileSize)
                throw new BadRequestException("Image.TooLarge", "Maximum file size is 5MB.");
            var uploadsPath = Path.Combine(
           environment.ContentRootPath,
           "uploads",
           folderName);
            Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(uploadsPath, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            return $"/uploads/profile/{fileName}";
        }

        public Task DeleteAsync(string imageUrl, string folderName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return Task.CompletedTask;

            var fileName = Path.GetFileName(imageUrl);
            var uploadsPath = Path.Combine(
        environment.ContentRootPath,
        "uploads",
        folderName);
            var fullPath = Path.Combine(uploadsPath, fileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }
    }
}
