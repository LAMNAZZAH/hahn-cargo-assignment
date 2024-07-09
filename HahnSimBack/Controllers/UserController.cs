using HahnCargoAutomation.Server.Entities;
using HahnCargoAutomation.Server.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace HahnCargoAutomation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ILogger<UserController> logger) : ControllerBase
    {
        private readonly ILogger<UserController> logger = logger;


        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout(SignInManager<AppUser> signInManager)
        {
            try
            {
                await signInManager.SignOutAsync();
                var response = ApiResponse<string>.SuccessResponse("", "logged out successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while processing your request", HttpStatusCode.InternalServerError, new List<string> { ex.Message }));
            }
        }


        [HttpGet("pingauth")]
        public IActionResult PingAuth()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    logger.LogWarning("Unauthorized access attempt in PingAuth");
                    return Unauthorized(ApiResponse<object>.ErrorResponse("You are not logged in", HttpStatusCode.Unauthorized, ["Can't find Your User"]));
                }

                var email = User.FindFirstValue(ClaimTypes.Email);
                logger.LogInformation("PingAuth successful for user with email {Email}", email);
                var response = ApiResponse<object>.SuccessResponse(new { Email = email }, "You are logged in");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while processing your request", HttpStatusCode.InternalServerError, new List<string> { ex.Message }));
            }
        }
    }
}

