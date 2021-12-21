using Avander.VehicleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Contracts.Persistence
{
    public interface IMeasurementPointRepository : IAsyncRepository<MeasurementPoint>
    {
        Task<MeasurementPoint> GetByName(string name);
    }
}
