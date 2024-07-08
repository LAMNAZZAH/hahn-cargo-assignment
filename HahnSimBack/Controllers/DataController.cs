using HahnCargoAutomation.Server.Infrastructure;
using HahnSimBack.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Claims;

namespace HahnSimBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController(ICargoSimService cargoSimService, ICachingTokenService cachingTokenService) : ControllerBase
    {
        private readonly ICargoSimService cargoSimService = cargoSimService;
        private readonly ICachingTokenService cachingTokenService = cachingTokenService;

        [HttpGet("/coins")]
        public async Task<IActionResult> GetCoins()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(ApiResponse<object>.ErrorResponse("You are not logged in", HttpStatusCode.Unauthorized, ["Can't find Your User"]));
                }
                var username = User.FindFirstValue(ClaimTypes.Email);
                var token = await cachingTokenService.GetTokenAsync(username, "Hahn");
                var data = await cargoSimService.GetDataAsync<Int32>("/User/CoinAmount", token);
                return Ok(ApiResponse<int>.SuccessResponse(data, ""));

            } catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message, HttpStatusCode.Unauthorized, new List<string> { "Can't find Your User" }));
            } catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while processing your request", HttpStatusCode.InternalServerError, new List<string> { ex.Message }));
            }
        }
    }
}
