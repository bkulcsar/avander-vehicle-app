using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Measurements.Commands
{
    public class UploadMeasurementsCsvCommand : IRequest
    {
        public IFormFileCollection CsvFiles { get; set; }
    }
}
