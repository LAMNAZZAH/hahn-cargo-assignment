namespace HahnSimBack.Interfaces
{
    public interface ICachingTokenService
    {
        public Task<string> GetTokenAsync(string username, string password);
        public Task<string> FetchTokenAsync(string email, string password);
        public void InvalidateToken();
    }
}
