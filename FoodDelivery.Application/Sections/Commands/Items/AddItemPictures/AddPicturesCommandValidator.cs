using FluentValidation;

namespace FoodDelivery.Application.Menus.Commands.Items.AddItemPictures
{
    public sealed class AddPicturesCommandValidator
        : AbstractValidator<AddItemPicturesCommand>
    {
        public AddPicturesCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty()
                .WithMessage("Restaurant Id is required.");

            RuleFor(x => x.SectionId)
                .NotEmpty()
                .WithMessage("SectionId is required.");

            RuleFor(x => x.ItemId)
                .NotEmpty()
                .WithMessage("ItemId is required.");

            RuleFor(x => x.Pictures)
                .NotNull()
                .WithMessage("Pictures collection is required.")
                .Must(p => p.Any())
                .WithMessage("At least one picture must be provided.")
                .Must(p => p.Count() <= 5)
                .WithMessage("You can upload a maximum of 5 pictures at once.");
        }
    }
}