using Avander.VehicleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Contracts.Persistence
{
    public interface IMeasurementRepository : IAsyncRepository<Measurement>
    {
        Task<Measurement> GetUniqueByDate(int vehicleId, int shopId, int measurementPointId, DateTime date);
        Task<List<Measurement>> GetAllWithParentsPaged(int page = 1, int size = 50);
        Task<List<Measurement>> GetAllPaged(int page = 1, int size = 50);
        int GetTotalCount();
    }
}
