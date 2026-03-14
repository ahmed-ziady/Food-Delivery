using FluentValidation;
using FoodDelivery.Application.Account.Commands.Address.Commands.SetAddress;

namespace FoodDelivery.Application.Account.Commands.Address.Validators
{
    public sealed partial class AddressValidatore : AbstractValidator<SetAddressCommand>
    {
        public AddressValidatore()
        {
            RuleFor(x => x.UserId)
               .NotEmpty()
               .WithMessage("UserId is required.");

            RuleFor(x => x.Street)
                .NotEmpty()
                .WithMessage("Street is required.")
                .MaximumLength(100);

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .WithMessage("Postal code is required.")
                .MaximumLength(20);

            RuleFor(x => x.AppartmentNumber)
                .NotEmpty()
                .WithMessage("Appartment number is required.")
                .MaximumLength(20);

            RuleFor(x => x.Lat)
                .InclusiveBetween(-90, 90)
                .WithMessage("Latitude must be between -90 and 90.");

            RuleFor(x => x.Lng)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude must be between -180 and 180.");

            RuleFor(x => x.Label)
                .IsInEnum()
                .WithMessage("Invalid address label.");
        }
    }
}
