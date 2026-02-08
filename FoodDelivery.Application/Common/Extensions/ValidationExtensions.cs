using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Common.Extensions
{
    public static class ValidationExtensions
    {
        private static readonly HashSet<string> InvalidDefaults =
            new(StringComparer.OrdinalIgnoreCase)
            {
            "string",
            "test",
            "example",
            "xxx"
            };

        public static IRuleBuilderOptions<T, string> NotDefaultPlaceholder<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(value =>
                !string.IsNullOrWhiteSpace(value) &&
                !InvalidDefaults.Contains(value.Trim())
            )
            .WithMessage("Value must be a meaningful text.");
        }
    }

}
