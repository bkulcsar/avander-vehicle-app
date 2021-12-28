using Avander.VehicleApp.Application.Features.Measurements.Commands;
using Avander.VehicleApp.Application.Features.Measurements.Queries;
using Avander.VehicleApp.Application.Models;
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
    public class MeasurementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeasurementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload", Name = "UploadCsv")]
        public async Task<ActionResult> UploadCsvAsync()
        {
            var files = HttpContext.Request.Form.Files;
            var uploadCommand = new UploadMeasurementsCsvCommand()
            {
                CsvFiles = files
            };
            await _mediator.Send(uploadCommand);
            return NoContent();
        }

        [HttpGet(Name = "GetMeasurements")]
        public async Task<ActionResult<List<MeasurementListVm>>> GetMeasurements([FromQuery] GetMeasurementListQuery query)
        {
            var result = await _mediator.Send(query);
            Response.Headers.Add("X-Total-Count", result.TotalCount.ToString());
            return Ok(result.MeasurementListVms);
        }

    }
}
