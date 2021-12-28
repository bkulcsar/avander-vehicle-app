using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Measurements.Queries
{
    public class GetMeasurementListQuery : IRequest<GetMeasurementListQueryResponse>
    {
        [FromQuery(Name = "expand")]
        public bool? Expand { get; set; }

        [FromQuery(Name = "page")]
        public int? Page { get; set; }

        [FromQuery(Name = "size")]
        public int? Size { get; set; }
    }
}
