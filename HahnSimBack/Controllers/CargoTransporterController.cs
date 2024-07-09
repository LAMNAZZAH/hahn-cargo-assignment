using HahnCargoAutomation.Server.Infrastructure;
using HahnSimBack.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HahnSimBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoTransporterController(ICargoSimService cargoSimService, ICachingTokenService cachingTokenService) : ControllerBase
    {
        private readonly ICargoSimService cargoSimService = cargoSimService;
        private readonly ICachingTokenService cachingTokenService = cachingTokenService;
/*
        [HttpGet]
        public async Task<IActionResult> GetCargoTransporter()
        {

        }*/
    }
}
