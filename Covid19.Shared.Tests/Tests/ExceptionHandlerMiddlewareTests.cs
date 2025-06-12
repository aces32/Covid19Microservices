using Covid19.Shared.Exceptions;
using Covid19.Shared.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Covid19.Shared.Tests
{
    public class ExceptionHandlerMiddlewareTests
    {
        [Fact]
        public async Task BadRequestException_Returns_BadRequest_With_Message()
        {
            var builder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseCustomExceptionHandler();
                    app.Run(_ => throw new BadRequestException("bad request"));
                });

            using var server = new TestServer(builder);
            using var client = server.CreateClient();

            var response = await client.GetAsync("/");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("bad request", body);
        }

        [Fact]
        public async Task NotFoundException_Returns_NotFound()
        {
            var builder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseCustomExceptionHandler();
                    app.Run(_ => throw new NotFoundException("Entity", 1));
                });

            using var server = new TestServer(builder);
            using var client = server.CreateClient();

            var response = await client.GetAsync("/");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
