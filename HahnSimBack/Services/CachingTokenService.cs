using HahnSimBack.Dtos;
using HahnSimBack.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Security.Claims;

namespace HahnSimBack.Services
{
    public class CachingTokenService(IMemoryCache cache, ICargoSimService cargoSimService, HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor) : ICachingTokenService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IMemoryCache cache = cache;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        private const string TokenCacheKeyPrefix = "CargoSimAuthToken_";

        public async Task<string> GetTokenAsync(string username, string password)
        {
            var TokenCacheKey = TokenCacheKeyPrefix + username;

            if (cache.TryGetValue(TokenCacheKey, out CacheTokenInfoDto CacheTokenInfo))
            {
                if (IsAuthenticatedUserValid(username) && CacheTokenInfo!.Token != string.Empty)
                {
                    return CacheTokenInfo.Token;
                }
                else
                {
                    InvalidateToken(username);
                }
            }

            var token = await FetchTokenAsync(username, "Hahn");
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromDays(7))
                .SetPriority(CacheItemPriority.High);

            cache.Set(TokenCacheKey, new CacheTokenInfoDto { Token = token, Username = username }, cacheEntryOptions);

            return token;
        }

        public async Task<string> FetchTokenAsync(string email, string password)
        {
            var response = await httpClient.PostAsJsonAsync("/User/Login", new { username = email, password });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
            return result.token;
        }

        public void InvalidateToken(string username)
        {
            var TokenCacheKey = TokenCacheKeyPrefix + username;
            cache.Remove(TokenCacheKey);
        }

        private bool IsAuthenticatedUserValid(string username)
        {
            var authenticatedUserName = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            return authenticatedUserName == username;
        }
    }
}
