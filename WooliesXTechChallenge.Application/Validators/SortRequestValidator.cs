using FluentValidation;
using System;

namespace WooliesXTechChallenge.Application.Validators
{
    /// <summary>
    /// Sort Request Validator validates Sort Option
    /// </summary>
    public class SortRequestValidator : AbstractValidator<string>
    {
        public SortRequestValidator()
        {
            // validate request
            RuleFor(r => r).NotNull()
                .WithMessage("Invalid request");

            // validate option
            RuleFor(r => Array.IndexOf(Constants.SORT_METHODS, r.ToLower())).GreaterThanOrEqualTo(0)
                .WithMessage("Invalid sort option");
        }
    }
}
