using Application.Feature.Buses.Commands;
using FluentValidation;

namespace Application.Feature.Buses.Validators
{
    public class UpdateBusCommandValidator : AbstractValidator<UpdateBusCommand>
    {
        public UpdateBusCommandValidator()
        {
            RuleFor(x => x.DriverId)
                .NotNull();

            RuleFor(x => x.RegistrationPlate)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}