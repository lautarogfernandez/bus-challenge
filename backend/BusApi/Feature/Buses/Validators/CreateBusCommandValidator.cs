using BusApi.Feature.Buses.Commands;
using FluentValidation;

namespace BusApi.Feature.Buses.Validators
{
    public class CreateBusCommandValidator : AbstractValidator<CreateBusCommand>
    {
        public CreateBusCommandValidator()
        {
            RuleFor(x => x.DriverId)
                .NotNull();

            RuleFor(x => x.RegistrationPlate)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}