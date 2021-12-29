using Avander.VehicleApp.Application.Features.Shops.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShopsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetShops")]
        public async Task<ActionResult<List<ShopListVm>>> GetMeasurements()
        {
            var result = await _mediator.Send(new GetShopListQuery());
            return Ok(result);
        }
    }
}
