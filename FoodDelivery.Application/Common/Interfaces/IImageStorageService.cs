using Microsoft.AspNetCore.Http;

namespace FoodDelivery.Application.Common.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> UploadAsync(
            IFormFile file,
            CancellationToken cancellationToken);

        Task DeleteAsync(
            string imageUrl,
            CancellationToken cancellationToken);
    }

}
