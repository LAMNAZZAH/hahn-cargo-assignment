using HahnCargoAutomation.Server.Infrastructure;
using HahnSimBack.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace HahnSimBack.Controllers
{
    public class BaseApiController(ICargoSimService cargoSimService, ICachingTokenService cachingTokenService) : ControllerBase
    {
        protected readonly ICargoSimService cargoSimService = cargoSimService;
        protected readonly ICachingTokenService cachingTokenService = cachingTokenService;

        protected async Task<IActionResult> ExecuteAuthenticatedRequestAsync<T>(Func<string, Task<T>> action)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse("You are not logged in", HttpStatusCode.Unauthorized, ["Can't find Your User"]));
            }

            try
            {
                var username = User.FindFirstValue(ClaimTypes.Email);
                var token = await cachingTokenService.GetTokenAsync(username, "Hahn");
                var data = await action(token);
                return Ok(ApiResponse<T>.SuccessResponse(data, ""));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message, HttpStatusCode.Unauthorized, new List<string> { "Can't find Your User" }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while processing your request", HttpStatusCode.InternalServerError, new List<string> { ex.Message }));
            }
        }
    }
}
