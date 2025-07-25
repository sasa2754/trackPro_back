using FluentValidation;
using TrackPro.Application.Contracts.Persistence;

namespace TrackPro.Application.Features.Parts.Commands.CreatePart
{
    public class CreatePartCommandValidator : AbstractValidator<CreatePartCommand>
    {
        private readonly IPartRepository _partRepository;

        public CreatePartCommandValidator(IPartRepository partRepository)
        {
            _partRepository = partRepository;

            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");

            RuleFor(p => p.Code)
                .MustAsync(IsCodeUnique).WithMessage("A part with the same code already exists.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Responsible)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> IsCodeUnique(string code, CancellationToken token)
        {
            var part = await _partRepository.GetByCodeAsync(code);
            return part == null;
        }
    }
}