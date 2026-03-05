using FluentValidation;
using FoodDelivery.Application.Ingredients.Commands.AddIngredients;

namespace FoodDelivery.Application.Admin.Commands.AddIngredients
{
    public sealed class AddIngredientsCommandValidator
        : AbstractValidator<AddIngredientCommand>
    {
        public AddIngredientsCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ingredient name is required.")
                .MaximumLength(100);

            RuleFor(x => x.IngredientType)
                .IsInEnum().WithMessage("Invalid ingredient type.");
            RuleFor(x => x.Picture)
          .Must(file => file == null || file.Length > 0)
          .WithMessage("Invalid picture file");
        }
    }
}