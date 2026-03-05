using FoodDelivery.Domain.Commons;
using FoodDelivery.Domain.Commons.Exceptions;
using FoodDelivery.Domain.Enums;
using FoodDelivery.Domain.ValueObjects;

namespace FoodDelivery.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public DeliveryType DeliveryType { get; private set; }

    public Guid MenuSectionId { get; private set; }
    public MenuSection MenuSection { get; private set; } = null!;

    private readonly List<Picture> _pictures = new();
    public IReadOnlyCollection<Picture> Pictures => _pictures;

    private readonly List<MenuItemIngredient> _menuItemIngredients = new();
    public IReadOnlyCollection<MenuItemIngredient> MenuItemIngredients => _menuItemIngredients;

    private const int MaxPictures = 5;

    private MenuItem() { }

    public MenuItem( string name, decimal price, Guid menuSectionId,
        string? description = null, DeliveryType deliveryType = 0)
    {
        Id = Guid.NewGuid();
        MenuSectionId = menuSectionId;

        SetName(name);
        SetPrice(price);
        SetDescription(description);
        SetDeliveryType(deliveryType);
    }

    public void UpdateDetails(string? name, decimal? price, string? description)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name.Trim();

        if (price.HasValue)
            Price = price.Value;

        if (!string.IsNullOrWhiteSpace(description))
            Description = description.Trim();
    }


    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Item name cannot be empty.");

        Name = name.Trim();
    }

    public void SetPrice(decimal price)
    {
        if (price < 0)
            throw new PriceMustBePositiveException(price);

        Price = price;
    }

    public void SetDescription(string? description)
        => Description = description?.Trim();

    public void SetDeliveryType(DeliveryType deliveryType)
        => DeliveryType = deliveryType;

    public void AddPictures(IEnumerable<Picture> pictures)
    {
        foreach (var picture in pictures)
        {
            if (_pictures.Any(p => p.Url == picture.Url))
                throw new DomainException("Picture already exists.");

            if (_pictures.Count >= MaxPictures)
                throw new TooManyImagesException(MaxPictures);

            _pictures.Add(picture);
        }
    }

    public void RemovePicture(string url)
    {
        var picture = _pictures.FirstOrDefault(p =>
            p.Url.Equals(url, StringComparison.OrdinalIgnoreCase))
            ?? throw new ImageNotFoundException(url);

        _pictures.Remove(picture);
    }

    public void AddIngredients(IEnumerable<Ingredient> ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            if (_menuItemIngredients.Any(mi => mi.IngredientId == ingredient.Id))
                throw new DomainException($"Ingredient '{ingredient.Name}' already exists.");

            _menuItemIngredients.Add(new MenuItemIngredient(Id, ingredient.Id));
        }
    }

    public void UpdateIngredients(IEnumerable<Ingredient> newIngredients)
    {
        var newIds = newIngredients.Select(i => i.Id).ToHashSet();

        _menuItemIngredients.RemoveAll(mi => !newIds.Contains(mi.IngredientId));

        foreach (var ingredient in newIngredients)
        {
            if (_menuItemIngredients.All(mi => mi.IngredientId != ingredient.Id))
                _menuItemIngredients.Add(new MenuItemIngredient(Id, ingredient.Id));
        }
    }

    public IEnumerable<Guid> Ingredients => _menuItemIngredients.Select(mi => mi.IngredientId);
}