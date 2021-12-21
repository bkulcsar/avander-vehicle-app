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
    public class ShopRepository : BaseRepository<Shop>, IShopRepository
    {
        private readonly List<Shop> ShopCache;
        
        public ShopRepository(VehicleDbContext dbContext) : base(dbContext)
        {
            ShopCache = new List<Shop>();
        }

        public async Task<Shop> GetByName(string name)
        {
            if (ShopCache.Exists(x => x.Name == name))
            {
                return ShopCache.Find(x => x.Name == name);
            }
            else
            {
                var shop = await _dbContext.Shops.Where(x => x.Name == name).FirstOrDefaultAsync();
                ShopCache.Add(shop);

                return shop;
            }
        }

        public override async Task<Shop> AddAsync(Shop entity)
        {
            await base.AddAsync(entity);
            ShopCache.Add(entity);

            return entity;
        }
    }
}
