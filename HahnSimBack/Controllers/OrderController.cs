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
    public class OrderController : BaseApiController
    {

        //private readonly IOrderService _orderService;


        public OrderController(ICargoSimService cargoSimService, ICachingTokenService cachingTokenService)//, IOrderService orderService)
            : base(cargoSimService, cachingTokenService)
        {
            //_orderService = orderService;
        }

        [HttpGet("GetAllAvailable")]
        public async Task<IActionResult> GetAllAvailableOrders()
        {
            return await ExecuteAuthenticatedRequestAsync(token =>
            cargoSimService.SendRequestAsync<List<OrderDto>>(HttpMethod.Get, "/Order/GetAllAvailable", token));
        }

        [HttpGet("GetAllAccepted")]
        public async Task<IActionResult> GetAllAcceptetdOrders()
        {
            return await ExecuteAuthenticatedRequestAsync(token =>
            cargoSimService.SendRequestAsync<List<OrderDto>>(HttpMethod.Get, "/Order/GetAllAccepted", token));
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder()
        {
            return await ExecuteAuthenticatedRequestAsync(token =>
            cargoSimService.SendRequestAsync<object>(HttpMethod.Post, "/Order/Create", token, new { ExpectedResponseBody = false }));
        }


        [HttpPost("AcceptOrder")]
        public async Task<IActionResult> AcceptOrder([FromQuery] int OrderId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "orderId", OrderId.ToString() }
            };

            return await ExecuteAuthenticatedRequestAsync(token =>
            cargoSimService.SendRequestAsync<object>(HttpMethod.Post, "/Order/Accept", token, new { ExpectedResponseBody = false }, queryParams));
        }
    }
}