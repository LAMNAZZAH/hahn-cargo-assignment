using HahnSimBack.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HahnSimBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : BaseApiController
    {
        public CoinController(ICargoSimService cargoSimService, ICachingTokenService cachingTokenService)
            : base(cargoSimService, cachingTokenService)
        {
        }

        [HttpGet("Get")]
        public Task<IActionResult> GetCoins()
        {
            return ExecuteAuthenticatedRequestAsync(token => 
            cargoSimService.SendRequestAsync<int>(HttpMethod.Get, "/User/CoinAmount", token));
        }
    }
}