using FoodDelivery.Domain.Commons;
using FoodDelivery.Domain.Commons.Exceptions;
using FoodDelivery.Domain.Enums;
using FoodDelivery.Domain.ValueObjects;

namespace FoodDelivery.Domain.Entities
{
    public class MenuItem
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public DeliveryType DeliveryType { get; private set; }

        private readonly List<Ingredient> _ingredients = [];
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients.AsReadOnly();

        private readonly List<Picture> _pictures = [];
        public IReadOnlyCollection<Picture> Pictures => _pictures.AsReadOnly();

        private MenuItem() { } // EF

        public MenuItem(Guid id, string name, decimal price, string? description = null)
        {
            Id = id;
            SetName(name);
            SetPrice(price);
            SetDescription(description);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Item name cannot be empty.");
            Name = name;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
                throw new PriceMustBePositiveException(price);
            Price = price;
        }

        public void SetDescription(string? description) => Description = description;
        //<< Ingredients Operations>>
        public void SetIngredients(List<Ingredient> ingredients)
        {
            var duplicate = ingredients
                .GroupBy(i => i.Name.Trim().ToLower())
                .FirstOrDefault(g => g.Count() > 1);

            if (duplicate != null)
                throw new DuplicateIngredientException(duplicate.Key);

            _ingredients.Clear();
            _ingredients.AddRange(ingredients);
        }

        public void AddIngredient(Ingredient ingredient)
        {
            if (_ingredients.Any(i => i.Name.Trim().Equals(ingredient.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new DuplicateIngredientException(ingredient.Name);
            _ingredients.Add(ingredient);
        }

        public void RemoveIngredient(string name)
        {
            var ingredient = _ingredients.FirstOrDefault(i => i.Name.Trim().Equals(name.Trim(), StringComparison.OrdinalIgnoreCase))
                             ?? throw new IngredientNotFoundException(name);
            _ingredients.Remove(ingredient);
        }

        public void SetImages(List<Picture> pictures)
        {
            if (pictures.Count > 5) throw new TooManyImagesException(5);

            _pictures.Clear();
            _pictures.AddRange(pictures);
        }

        public void AddImage(Picture picture)
        {
            if (_pictures.Count >= 5) throw new TooManyImagesException(5);
            _pictures.Add(picture);
        }

        public void RemoveImage(string url)
        {
            var picture = _pictures.FirstOrDefault(p => p.Url.Trim().Equals(url.Trim(), StringComparison.OrdinalIgnoreCase))
                          ?? throw new ImageNotFoundException(url);
            _pictures.Remove(picture);
        }
        public void UpdateDetails(string? name, decimal? price, string? description)
        {
            if (!string.IsNullOrWhiteSpace(name))
                SetName(name);

            if (price.HasValue)
                SetPrice(price.Value);

            if (!string.IsNullOrWhiteSpace(description))
                SetDescription(description);
        }

    }

}
