using Microsoft.AspNetCore.Http;

namespace FoodDelivery.Application.Common.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> UploadAsync(
            IFormFile file,
            string folderName,
            CancellationToken cancellationToken);

        Task DeleteAsync(
            string imageUrl,
            string folderName,
            CancellationToken cancellationToken);
    }

}
