using FoodDelivery.Domain.Commons;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.Domain.Entities;

public class Menu
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Guid RestaurantId { get; private set; }
    public User Restaurant { get; private set; } = null!;

    private readonly List<MenuSection> _sections = new();
    public IReadOnlyCollection<MenuSection> Sections => _sections;

    private Menu() { }

    public Menu(Guid id ,string name, Guid restaurantId)
    {
        Id =id;
        SetName(name);
        RestaurantId = restaurantId;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Menu name cannot be empty.");
        Name = name;
    }

    public void AddSection(MenuSection section)
    {
        if (_sections.Any(s => s.Name.Equals(section.Name, StringComparison.OrdinalIgnoreCase)))
            throw new DomainException($"Section '{section.Name}' already exists.");
        _sections.Add(section);
    }

    public void UpdateSectionName(Guid sectionId, string name)
    {
        var section = _sections.FirstOrDefault(s => s.Id == sectionId)
                      ?? throw new DomainException("Menu section not found.");
        if (_sections.Any(s => s.Id != sectionId && s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new DomainException($"Another menu section with name '{name}' already exists.");
        section.SetName(name);
    }

    public void RemoveSection(Guid sectionId)
    {
        var section = _sections.FirstOrDefault(s => s.Id == sectionId)
                      ?? throw new DomainException("Menu section not found.");
        _sections.Remove(section);
    }

    public MenuSection? GetSection(Guid sectionId) => _sections.FirstOrDefault(s => s.Id == sectionId);
}