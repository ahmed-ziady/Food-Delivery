//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailConfirm
//{
//    public class ChangeEmailConfirmCommandValidator:AbstractValidator<ChangeEmailConfirmCommand>
//    {
//        public ChangeEmailConfirmCommandValidator()
//        {
//            RuleFor(x => x.UserId)
//                .NotEmpty().WithMessage("UserId is required");
//             RuleFor(x => x.NewEmail)
//                .NotEmpty()
//                .EmailAddress().WithMessage("A valid email address is required");
//            RuleFor(x => x.Otp)
//                .NotEmpty().Length(6).WithMessage("Otp must be 6 characters long");
//        }
//    }
//}
