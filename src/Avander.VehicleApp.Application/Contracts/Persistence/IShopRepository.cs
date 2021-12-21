using Avander.VehicleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Contracts.Persistence
{
    public interface IShopRepository : IAsyncRepository<Shop>
    {
        Task<Shop> GetByName(string name);
    }
}
