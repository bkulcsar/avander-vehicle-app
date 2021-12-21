using Avander.VehicleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Contracts.Persistence
{
    public interface IVehicleRepository : IAsyncRepository<Vehicle>
    {
        Task<Vehicle> GetByJSNAndVehicleModel(string jsn, string vehicleModel);
    }
}
