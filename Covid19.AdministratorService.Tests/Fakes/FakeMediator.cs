using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Covid19.AdministratorService.Core.Features.LocationsHandler.Queries.GetLocationsByIdsHandler;
using Covid19.AdministratorService.Models.LocationModels;
using MediatR;

namespace Covid19.AdministratorService.Tests.Fakes;

internal class FakeMediator : IMediator
{
    private readonly List<AvailableLocationListDto> _locations;

    public FakeMediator(List<AvailableLocationListDto> locations) => _locations = locations;

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
    {
        if (request is GetLocationsByIdsQuery)
        {
            return Task.FromResult((TResponse)(object)_locations);
        }
        throw new NotImplementedException();
    }

    // unused members
    public Task<object?> Send(object request, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task Publish(object notification, CancellationToken cancellationToken = default) => Task.CompletedTask;
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification => Task.CompletedTask;
    public Task Publish<TNotification>(TNotification notification, PublishOptions options, CancellationToken cancellationToken = default) where TNotification : INotification => Task.CompletedTask;
}
