using HahnSimBack.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HahnSimBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController: BaseApiController
    {
        
        public SimulationController(ICargoSimService cargoSimService, ICachingTokenService cachingTokenService)
            : base(cargoSimService, cachingTokenService) 
        {
        }

        [HttpPost("Start")]
        public async Task<IActionResult> Start()
        {
            return await ExecuteAuthenticatedRequestAsync(token =>
            cargoSimService.SendRequestAsync<object>(HttpMethod.Post, "/Sim/Start", token, new { ExpectedResponseBody = false }));
        }

        [HttpPost("Stop")]
        public async Task<IActionResult> Stop()
        {
            return await ExecuteAuthenticatedRequestAsync(token =>
            cargoSimService.SendRequestAsync<object>(HttpMethod.Post, "/Sim/Stop", token, new { ExpectedResponseBody = false }));
        }
    }
}
