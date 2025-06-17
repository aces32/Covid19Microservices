using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace Covid19.IndividualService.Tests.Fakes;

internal class NoOpPublishEndpoint : IPublishEndpoint
{
    public List<object> Published { get; } = new();

    public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        Published.Add(message!);
        return Task.CompletedTask;
    }

    public Task Publish(object message, CancellationToken cancellationToken = default) => Task.CompletedTask;
    public Task Publish(object message, Type messageType, CancellationToken cancellationToken = default) => Task.CompletedTask;
}
