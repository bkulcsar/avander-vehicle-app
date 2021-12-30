using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Measurements.Queries.GetMeasurement
{
    public class GetMeasurementQuery : IRequest<MeasurementVm>
    {
        public int Id { get; set; }
    }
}
