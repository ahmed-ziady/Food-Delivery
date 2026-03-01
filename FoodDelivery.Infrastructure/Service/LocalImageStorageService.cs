using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FoodDelivery.Infrastructure.Service
{
    public sealed class LocalImageStorageService(IWebHostEnvironment environment) : IImageStorageService
    {
        private const int MaxFileSize = 5 * 1024 * 1024;

        private readonly string _profileUploadsPath =
            Path.Combine(environment.ContentRootPath, "uploads", "profile"); 

        public async Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
                throw new BadRequestException("Image.Empty", "File is empty.");

            if (!file.ContentType.StartsWith("image/"))
                throw new BadRequestException("Image.InvalidType", "Only image files are allowed.");

            if (file.Length > MaxFileSize)
                throw new BadRequestException("Image.TooLarge", "Maximum file size is 5MB.");

            Directory.CreateDirectory(_profileUploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(_profileUploadsPath, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            return $"/uploads/profile/{fileName}";
        }

        public Task DeleteAsync(string imageUrl, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return Task.CompletedTask;

            var fileName = Path.GetFileName(imageUrl);
            var fullPath = Path.Combine(_profileUploadsPath, fileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }
    }
}
