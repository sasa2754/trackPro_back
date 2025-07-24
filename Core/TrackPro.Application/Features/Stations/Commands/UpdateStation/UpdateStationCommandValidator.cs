using FluentValidation;

namespace TrackPro.Application.Features.Stations.Commands.UpdateStation
{
    public class UpdateStationCommandValidator : AbstractValidator<UpdateStationCommand>
    {
        public UpdateStationCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Order)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}