using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.UpdateProfileImage
{
    public class UploadProfileImageValidator : AbstractValidator<UploadProfileImageCommand>
    {
        public UploadProfileImageValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("User ID cannot be empty.");
            RuleFor(x => x.File)
                .NotNull().WithMessage("Profile image file is required.")
                .Must(file => file.Length > 0).WithMessage("Profile image file cannot be empty.")
                .Must(file => file.ContentType.StartsWith("image/")).WithMessage("Only image files are allowed.");
        }
    }
}
