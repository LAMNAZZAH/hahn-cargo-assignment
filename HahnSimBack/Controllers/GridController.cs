using HahnCargoAutomation.Server.Infrastructure;
using HahnSimBack.Dtos;
using HahnSimBack.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using System.Net;

namespace HahnSimBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GridController : BaseApiController
    {

        private readonly IGridService _gridService;


        public GridController(ICargoSimService cargoSimService, ICachingTokenService cachingTokenService, IGridService gridService)
            : base(cargoSimService, cachingTokenService)
        {
            _gridService = gridService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetGridDataAsync()
        {
            return await ExecuteAuthenticatedRequestAsync(token =>
            cargoSimService.SendRequestAsync<GridDataResDto>(HttpMethod.Get, "/Grid/Get", token));
        }

        [HttpGet("SaveGrid")]
        public async Task<IActionResult> SaveGridDataAsync()
        {
            try
            {
                var gridResult = await GetGridDataAsync();
                if (gridResult is OkObjectResult okResult && okResult.Value is ApiResponse<GridDataResDto> apiResponse)
                {
                    Console.WriteLine($"entered here: {apiResponse.Data}");
                    var gridData = apiResponse.Data;
                    await _gridService.SaveGridDataAsync(gridData);
                    return Ok(gridData);
                }
                return BadRequest("error saving grid data!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while processing your request", HttpStatusCode.InternalServerError, new List<string> { ex.Message }));
            }
        }
    }
}