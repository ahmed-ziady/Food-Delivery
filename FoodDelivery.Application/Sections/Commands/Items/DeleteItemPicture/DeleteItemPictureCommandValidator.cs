using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Items.DeleteItemPicture
{
    public sealed class DeleteItemPictureCommandValidator:AbstractValidator<DeleteItemPictureCommand>
    {
        public DeleteItemPictureCommandValidator() {
            RuleFor(x => x.RestaurantId)
                   .NotEmpty()
                   .WithMessage("Restaurant Id is required.");

            RuleFor(x => x.SectionId)
                .NotEmpty()
                .WithMessage("Section Id is required.");

            RuleFor(x => x.ItemId)
                .NotEmpty()
                .WithMessage("ItemId is required.");
            RuleFor(x => x.Url)
                .NotEmpty()
                .WithMessage("Item url is required");
        }
    }
}
