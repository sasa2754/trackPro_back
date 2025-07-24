using FluentValidation;

namespace TrackPro.Application.Features.Stations.Commands.CreateStation
{
    public class CreateStationCommandValidator : AbstractValidator<CreateStationCommand>
    {
        public CreateStationCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Order)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}