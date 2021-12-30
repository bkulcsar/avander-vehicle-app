using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Measurements.Commands.DeleteMeasurement
{
    public class DeleteMeasurementCommand : IRequest
    {
        public int Id { get; set; }
    }
}
