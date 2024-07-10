using HahnCargoAutomation.Server.Infrastructure;
using HahnSimBack.Dtos;
using HahnSimBack.Entities;
using HahnSimBack.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HahnSimBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoTransporterController: BaseApiController
    {
        private readonly ICargoTransporterService _cargoTransporterService;

        public CargoTransporterController(ICargoSimService cargoSimService, ICachingTokenService cachingTokenService, ICargoTransporterService cargoTransporterService)
            : base(cargoSimService, cachingTokenService) 
        {
            _cargoTransporterService = cargoTransporterService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetCargoTransporters()
        {
            try
            {
                var cargoTransportersIds = await _cargoTransporterService.GetCargoTransporters();

                var allTransporters = new List<CargoTransporterDto>();

                foreach (var id in cargoTransportersIds)
                {
                    var queryParams = new Dictionary<string, string>
                {
                    {"transporterId", id.ToString() }
                };
                    var getCargoTransporterResponse = await ExecuteAuthenticatedRequestAsync(token =>
                               cargoSimService.SendRequestAsync<CargoTransporterDto>(HttpMethod.Get, "/CargoTransporter/Get", token, null, queryParams));

                    if (getCargoTransporterResponse is OkObjectResult okResult && okResult.Value is ApiResponse<CargoTransporterDto> apiResponse)
                    {
                        var cargoTransporter = apiResponse.Data;
                        if (cargoTransporter != null)
                        {
                            allTransporters.Add(cargoTransporter);
                        }
                        else
                        {
                            return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while processing your request", HttpStatusCode.InternalServerError, ["Oops! failed to fetch one transporter!"]));
                        }
                    }
                }
                return Ok(ApiResponse<List<CargoTransporterDto>>.SuccessResponse(allTransporters, "Cargo Transporters was fetched successfully!"));
            } 
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while processing your request", HttpStatusCode.InternalServerError, new List<string> { ex.Message }));
            }
        }

        [HttpPut("Move")]
        public async Task<IActionResult> MoveTransporter([FromQuery] int transporterId, [FromQuery] int targetNodeId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "transporterId", transporterId.ToString() },
                { "targetNodeId", targetNodeId.ToString() }
            };

            return await ExecuteAuthenticatedRequestAsync(token =>
            cargoSimService.SendRequestAsync<object>(HttpMethod.Put, "/CargoTransporter/Move", token, new { ExpectedResponseBody = false }, queryParams));
        }

        [HttpPost("Buy")]
        public async Task<IActionResult> BuyCargoTransporter([FromBody] BuyOrderReqDto buyOrderReqDto)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "positionId", buyOrderReqDto.PositionNodeId.ToString() }
            };

            var buyCargoTransporterResponse = await ExecuteAuthenticatedRequestAsync(token =>
            cargoSimService.SendRequestAsync<int>(HttpMethod.Post, "/CargoTransporter/Buy", token, null, queryParams));
            try
            {
                if (buyCargoTransporterResponse is OkObjectResult okResult && okResult.Value is ApiResponse<int> apiResponse)
                {
                    var cargoTransporterId = apiResponse.Data;
                    if (cargoTransporterId >= 0)
                    {
                        await _cargoTransporterService.SaveCargoTransporter(cargoTransporterId);
                        return Ok(ApiResponse<int>.SuccessResponse(cargoTransporterId, "Cargo Transporter was bought successfully!"));
                    }
                }
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while processing your request", HttpStatusCode.InternalServerError, ["buy transporter request failed"]));
            } 
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while processing your request", HttpStatusCode.InternalServerError, new List<string> { ex.Message }));
            }
        }
    }
}
