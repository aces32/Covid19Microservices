using FluentValidation;

namespace Covid19.IndividualLabsService.Core.Features.Command.SetTestOutcome
{
    public class SetTestOutcomeValidator : AbstractValidator<SetTestOutcomeCommand>
    {
        public SetTestOutcomeValidator()
        {
            RuleFor(p => p.IndividualId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(0);

            RuleFor(p => p.TestCompleted)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p.TestOutCome)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(e => e)
            .MustAsync(IsValidTestOutcomeType)
            .WithMessage("Test Outcome must be either negative or positive.");
        }

        private Task<bool> IsValidTestOutcomeType(SetTestOutcomeCommand e, CancellationToken token)
        {
            var isValid = TestOutcomeData.TestOutcomeType.Contains(e.TestOutCome.ToLower());
            return Task.FromResult(isValid);

        }
    }
}
