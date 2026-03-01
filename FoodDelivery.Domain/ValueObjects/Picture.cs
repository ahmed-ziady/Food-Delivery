using FoodDelivery.Domain.Commons;

namespace FoodDelivery.Domain.ValueObjects
{
    public class Picture
    {
        public string Url { get; private set; } = null!;

        private Picture() { }
        public Picture(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new DomainException("Picture URL cannot be empty.");
            Url = url;
        }
    }
}
