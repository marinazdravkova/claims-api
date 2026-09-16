using FluentValidation;

namespace Claims.Validators
{
    public class CoverValidator : AbstractValidator<Cover>
    {
        public CoverValidator()
        {
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                                     .WithMessage("StartDate cannot be in the past");


            RuleFor(x => x).Must(x => (x.EndDate.Date - x.StartDate.Date).TotalDays <= 365)
                           .WithMessage("Total insurance period cannot exceed 1 year");
        }
    }
}
