using FluentValidation;
using FoodDelivery.Application.Sections.Commands.Items.UpdateItem;

namespace FoodDelivery.Application.Menus.Commands.Items.UpdateItem
{
    public class UpdateItemCommnadValidator : AbstractValidator<UpdateItemCommand>
    {


        public UpdateItemCommnadValidator()
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

            RuleFor(x => x.Name)
                .MaximumLength(50)
                .WithMessage("Item name must not exceed 50 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Description)
                .MaximumLength(250)
                .WithMessage("Description must not exceed 250 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.")
                .When(x => x.Price.HasValue);
        }
    }

}
