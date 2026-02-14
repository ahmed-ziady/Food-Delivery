namespace FoodDelivery.Domain.Entities
{
    public class Menu
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string? Description { get; private set; }

        public Guid UserId { get; private set; }  // Identity user

        public double AverageRating { get; private set; }
        public int RatingCount { get; private set; }

        public List<MenuSection> Sections { get; private set; } = [];

        private Menu() { } // For EF

        public Menu(string name, string? description, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            UserId = userId;
            AverageRating = 0;
            RatingCount = 0;
        }

        public void AddSection(string name, string? description)
        {
            Sections.Add(new MenuSection(name, description));
        }

        public void UpdateRating(double newRating)
        {
            RatingCount++;
            AverageRating = ((AverageRating * (RatingCount - 1)) + newRating) / RatingCount;
        }
    }
}
