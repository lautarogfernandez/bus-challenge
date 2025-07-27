using BusApi.Feature.Drivers.Commands;
using FluentValidation;

namespace BusApi.Feature.Drivers.Validators
{
    public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
    {
        public CreateDriverCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.DocumentNumber)
                .NotEmpty()
                .MaximumLength(8)
                .Matches(@"^\d+$").WithMessage("DocumentNumber must contain only numbers.");
        }
    }
}