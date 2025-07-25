using Application.Feature.Kids.Commands;
using FluentValidation;

namespace Application.Feature.Kids.Validators
{
    public class CreateKidCommandValidator : AbstractValidator<CreateKidCommand>
    {
        public CreateKidCommandValidator()
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