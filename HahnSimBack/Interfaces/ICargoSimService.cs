using HahnSimBack.Dtos;
using Microsoft.Extensions.Options;

namespace HahnSimBack.Interfaces
{
    public interface ICargoSimService
    {
        public Task<T> GetDataAsync<T>(string endpoint, string token);
        public Task<T> SendRequestAsync<T>(HttpMethod method, string endpoint, string token, object content = null, Dictionary<string, string> queryParams = null);
    }
}
