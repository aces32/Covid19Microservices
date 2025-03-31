using FluentValidation.Results;

namespace Covid19.Shared.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<string> ValidationErrors { get; }

        public CustomValidationException(IEnumerable<ValidationFailure> failures)
        {
            ValidationErrors = failures.Select(f => f.ErrorMessage).ToList();
        }
    }
}
