using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Persistence.Repositories
{
    public class MeasurementPointRepository : BaseRepository<MeasurementPoint>, IMeasurementPointRepository
    {
        private readonly List<MeasurementPoint> MeasurementPointCache;
        public MeasurementPointRepository(VehicleDbContext dbContext) : base(dbContext)
        {
            MeasurementPointCache = new List<MeasurementPoint>();
        }

        public async Task<MeasurementPoint> GetByName(string name)
        {
            if (MeasurementPointCache.Exists(x => x.Name == name))
            {
                return MeasurementPointCache.Find(x => x.Name == name);
            }
            else
            {
                var measurementPoint = await _dbContext.MeasurementPoints.Where(x => x.Name == name).FirstOrDefaultAsync();

                if (measurementPoint != null)
                {
                    MeasurementPointCache.Add(measurementPoint);
                }

                return measurementPoint;
            }
        }

        public override async Task<MeasurementPoint> AddAsync(MeasurementPoint entity)
        {
            await base.AddAsync(entity);
            MeasurementPointCache.Add(entity);

            return entity;
        }
    }
}
