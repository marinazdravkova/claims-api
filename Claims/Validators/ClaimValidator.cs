using FluentValidation;
using FluentValidation.AspNetCore;

namespace Claims.Validators
{
    public class ClaimValidator : AbstractValidator<Claim>
    {
        public ClaimValidator()
        {
            RuleFor(x => x.DamageCost).LessThanOrEqualTo(100000m)
                                      .WithMessage("DamageCost cannot exceed 100.000");
        }
    }
}
