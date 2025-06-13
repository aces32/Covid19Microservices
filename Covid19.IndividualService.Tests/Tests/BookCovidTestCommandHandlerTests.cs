using Covid19.IndividualService.Core.Features.Command.BookCovidTest;
using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Domain.Entities;
using Covid19.Shared.Exceptions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MediatR;
using MassTransit;
using System;
using System.Threading;
using Covid19.IndividualService.Tests.Fakes;
using Xunit;

namespace Covid19.IndividualService.Tests
{
    public class BookCovidTestCommandHandlerTests
    {
        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Covid19.IndividualService.Core.Profiles.MappingProfiles>());
            return config.CreateMapper();
        }

        [Fact]
        public async Task Handle_ThrowsNotFound_WhenBookingAvailabilityMissing()
        {
            var command = new BookCovidTestCommand
            {
                LocationId = 1,
                BookingDate = DateTime.UtcNow,
                IndividualEmailAddress = "a@test.com",
                IndividualFirstName = "A",
                IndividualLastName = "B",
                IndividualMobileNumber = "123",
                IndividualLab = new IndividualLabsRequest{ TestType = TestType.PCRTest }
            };
            var handler = new BookCovidTestCommandHandler(CreateMapper(), new NoOpLogger<BookCovidTestCommandHandler>(),
                new FakeIndividualRepository(), new FakeBookingAvailabilityRepository(null), new NoOpPublishEndpoint());

            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}

