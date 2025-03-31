using MediatR;

namespace Covid19.AdministratorService.Core.Features.Administrators.Queries.GetAvailableBookingDetails
{
    public class GetAvailableBookingDetailsQuery : IRequest<List<GetAvailableBookingDetailsQueryResponse>>
    {
    }
}
