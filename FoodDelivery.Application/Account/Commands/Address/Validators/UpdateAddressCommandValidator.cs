using FluentValidation;
using FoodDelivery.Application.Account.Commands.Address.Commands.UpdateAddress;
using FoodDelivery.Application.Common.Extensions;

namespace FoodDelivery.Application.Account.Commands.Address.Validators
{
    public sealed partial class AddressValidatore
    {
        public sealed class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
        {
            public UpdateAddressCommandValidator()
            {
                RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Address is required.");

                RuleFor(x => x.Street).NotDefaultPlaceholder().When(x => !string.IsNullOrWhiteSpace(x.Street))

                    .WithMessage("Street is required.")
                    .MaximumLength(100);

                RuleFor(x => x.PostalCode)
                  .NotDefaultPlaceholder().When(x => !string.IsNullOrWhiteSpace(x.Street))
                    .WithMessage("Postal code is required.")
                    .MaximumLength(20);

                RuleFor(x => x.AppartmentNumber)
                  .NotDefaultPlaceholder().When(x => !string.IsNullOrWhiteSpace(x.Street))
                    .WithMessage("Appartment number is required.")
                    .MaximumLength(20);
                RuleFor(x => x.Lat)

        .InclusiveBetween(-90, 90)
                .When(x => x.Lat.HasValue)
                .WithMessage("Latitude must be between -90 and 90.");

                RuleFor(x => x.Lng)
                    .InclusiveBetween(-180, 180)
                    .When(x => x.Lng.HasValue)
                    .WithMessage("Longitude must be between -180 and 180.");

                RuleFor(x => x.Label)
                    .IsInEnum()
                    .When(x => x.Label.HasValue)
                    .WithMessage("Invalid address label.");
            }
        }
    }
}
