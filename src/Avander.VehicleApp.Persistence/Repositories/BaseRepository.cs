using Avander.VehicleApp.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly VehicleDbContext _dbContext;

        public BaseRepository(VehicleDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            var entry = _dbContext.Entry(entity);

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(entity, null) == null)
                {
                    var prop = entry.Metadata.GetProperties().Where(prop => prop.Name == property.Name).FirstOrDefault();
                    if (prop != null)
                    {
                        entry.Property(property.Name).IsModified = false;
                    }
                    else
                    {
                        var nav = entry.Metadata.GetNavigations().Where(nav => nav.Name == property.Name).FirstOrDefault();
                        if (nav != null)
                        {
                            entry.Navigation(property.Name).IsModified = false;
                        }
                    }    
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
