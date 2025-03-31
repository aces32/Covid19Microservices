using FluentValidation;

namespace Covid19.LocationService.Core.Features.Locations.Queries.GetLocationsByIds
{
    public class GetLocationsByIdsQueryValidator : AbstractValidator<GetLocationsByIdsQuery>
    {
        public GetLocationsByIdsQueryValidator()
        {
            RuleFor(p => p.LocationIds)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }

    }
}
