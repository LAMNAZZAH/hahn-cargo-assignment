using HahnSimBack.Dtos;
using HahnSimBack.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HahnSimBack.Services
{
    public class CargoSimService(HttpClient httpClient, IOptions<CargoSimClientOptionsDto> options) : ICargoSimService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IOptions<CargoSimClientOptionsDto> options = options;


        public async Task<T> GetDataAsync<T>(string endpoint, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync(endpoint);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> SendRequestAsync<T>(HttpMethod method, string endpoint, string token, object content = null)
        {
            var request = new HttpRequestMessage(method, endpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (content != null && (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Patch))
            {
                request.Content = new StringContent(JsonSerializer.Serialize(content), System.Text.Encoding.UTF8, "application/json");
            }

            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Token expired or invalid");
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
