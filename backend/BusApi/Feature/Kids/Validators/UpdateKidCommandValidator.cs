using BusApi.Feature.Kids.Commands;
using FluentValidation;

namespace BusApi.Feature.Kids.Validators
{
    public class UpdateKidCommandValidator : AbstractValidator<UpdateKidCommand>
    {
        public UpdateKidCommandValidator()
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