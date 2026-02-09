using FoodDelivery.Domain.Common.Models;
using FoodDelivery.Domain.Common.ValueObjects;
using FoodDelivery.Domain.DinnerAggregate.ValueObjects;
using FoodDelivery.Domain.HostAggregate.ValueObjects;
using FoodDelivery.Domain.MenuAggregate.Entities;
using FoodDelivery.Domain.MenuAggregate.ValueObjects;
using FoodDelivery.Domain.MenuReview.ValueObjects;

namespace FoodDelivery.Domain.MenuAggregate
{
    public sealed class Menu : AggregateRoot<MenuId>
    {
        private readonly List<MenuSection> _sections = [];
        private readonly List<MenuReviewId> _menuReviewIds = [];
        private readonly List<DinnerId> _dinnerIds = [];

        private Menu() { }

        private Menu(
            MenuId id,
            string name,
            string description,
            HostId hostId) : base(id)
        {
            Name = name;
            Description = description;
            HostId = hostId;
            AverageRating = AverageRating.Empty();
            CreatedDateTime = DateTime.UtcNow;
            UpdatedDateTime = CreatedDateTime;
        }

        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public HostId HostId { get; private set; } = null!;
        public AverageRating AverageRating { get; private set; } = null!;

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        public IReadOnlyCollection<MenuSection> Sections => _sections.AsReadOnly();
        public IReadOnlyCollection<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
        public IReadOnlyCollection<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();

        // ---------- Factory ----------
        public static Menu Create(
            string name,
            string description,
            HostId hostId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Menu name is required.", nameof(name));

            ArgumentNullException.ThrowIfNull(hostId);

            return new Menu(
                MenuId.CreateUnique(),
                name.Trim(),
                description?.Trim() ?? string.Empty,
                hostId);
        }

        // ---------- Behavior ----------
        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Menu name is required.", nameof(name));

            Name = name.Trim();
            Touch();
        }

        public void UpdateDescription(string description)
        {
            Description = description?.Trim() ?? string.Empty;
            Touch();
        }

        public void AddSection(MenuSection section)
        {
            ArgumentNullException.ThrowIfNull(section);

            if (_sections.Any(s =>
                s.Name.Equals(section.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Section with same name already exists.");

            _sections.Add(section);
            Touch();
        }

        public void RemoveSection(MenuSection section)
        {
            ArgumentNullException.ThrowIfNull(section);

            if (!_sections.Remove(section))
                throw new InvalidOperationException("Section not found.");

            Touch();
        }

        public void AddReview(MenuReviewId reviewId, Rating rating)
        {
            ArgumentNullException.ThrowIfNull(reviewId);
            ArgumentNullException.ThrowIfNull(rating);

            if (_menuReviewIds.Contains(reviewId))
                return;

            _menuReviewIds.Add(reviewId);
            AverageRating = AverageRating.AddRating(rating);
            Touch();
        }

        public void RemoveReview(MenuReviewId reviewId, Rating rating)
        {
            if (!_menuReviewIds.Remove(reviewId))
                throw new InvalidOperationException("Review not found.");

            AverageRating = AverageRating.RemoveRating(rating);
            Touch();
        }

        public void AddDinner(DinnerId dinnerId)
        {
            ArgumentNullException.ThrowIfNull(dinnerId);

            if (_dinnerIds.Contains(dinnerId))
                return;

            _dinnerIds.Add(dinnerId);
            Touch();
        }

        private void Touch()
        {
            UpdatedDateTime = DateTime.UtcNow;
        }
    }

}
