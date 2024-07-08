using HahnSimBack.Dtos;
using HahnSimBack.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Security.Claims;

namespace HahnSimBack.Services
{
    public class CachingTokenService(IMemoryCache cache, ICargoSimService cargoSimService, HttpClient httpClient) : ICachingTokenService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IMemoryCache cache = cache;
        private readonly ICargoSimService cargoSimService = cargoSimService;
        private const string TokenCacheKey = "CargoSimAuthToken";

        public async Task<string> GetTokenAsync(string username, string password)
        {
            if (cache.TryGetValue(TokenCacheKey, out string cachedToken))
            {
                return cachedToken;
            }

            var token = await FetchTokenAsync(username, "Hahn");
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromDays(7))
                .SetPriority(CacheItemPriority.High);

            cache.Set(TokenCacheKey, token, cacheEntryOptions);

            return token;
        }

        public async Task<string> FetchTokenAsync(string email, string password)
        {
            var response = await httpClient.PostAsJsonAsync("/User/Login", new { username = email, password });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
            return result.token;
        }

        public void InvalidateToken()
        {
            cache.Remove(TokenCacheKey);
        }
    }
}
