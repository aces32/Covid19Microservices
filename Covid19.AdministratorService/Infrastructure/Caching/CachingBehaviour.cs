using Covid19.AdministratorService.Core.Contracts.Infrastructure.Cache;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Covid19.AdministratorService.Infrastructure.Caching
{
    public sealed class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>, ICachableMediatrQuery
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;

        public CachingBehavior(IDistributedCache cache, ILogger<TResponse> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;
            if (request.BypassCache)
            {
                return await next();
            }

            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();
                TimeSpan? slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromSeconds(30);
                var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
                var serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(response));
                await _cache.SetAsync(request.CacheKey, serializedData, options, cancellationToken);
                return response;
            }

            var cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
            if (cachedResponse != null)
            {
                var responseString = Encoding.UTF8.GetString(cachedResponse).ToString();
                response = JsonSerializer.Deserialize<TResponse>(responseString) ?? throw new InvalidOperationException("Deserialization returned null.");
                _logger.LogInformation($"Fetched from Cache -> '{request.CacheKey}'.");
            }
            else
            {
                response = await GetResponseAndAddToCache();
                _logger.LogInformation($"Added to Cache -> '{request.CacheKey}'.");
            }
            return response;
        }
    }
}
