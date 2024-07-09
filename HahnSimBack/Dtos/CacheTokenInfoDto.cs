using System.Security;

namespace HahnSimBack.Dtos
{
    public class CacheTokenInfoDto
    {
        public string Token {  get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
