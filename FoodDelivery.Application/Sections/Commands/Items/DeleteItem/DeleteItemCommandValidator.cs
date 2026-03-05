using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Items.DeleteItem
{
    public sealed class DeleteItemCommandValidator :AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemCommandValidator()
        {
            RuleFor(x => x.RestuarantId)
                .NotEmpty()
                .WithMessage("Restaurant Id is required.");

            RuleFor(x => x.SectionId)
                .NotEmpty()
                .WithMessage("SectionId is required.");
            RuleFor(x => x.ItemId)
                .NotEmpty()
                .WithMessage("Item Id is required");
        }
    }
}
