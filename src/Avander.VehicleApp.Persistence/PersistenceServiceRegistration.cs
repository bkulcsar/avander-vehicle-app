using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VehicleDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AvanderVehicleConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IMeasurementPointRepository, MeasurementPointRepository>();
            services.AddScoped<IMeasurementRepository, MeasurementRepository>();

            return services;
        }
    }
}
