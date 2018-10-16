namespace CarShop.Services.Admin.Implementations
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using AutoMapper.QueryableExtensions;

    using Data;
    using Data.EntityModels;
    using CarShop.Web.Data;

    using CarShop.Services.Admin.Contracts;


    public class DealerService : IDealerService
    {
        private readonly CarShopDbContext db;

        public DealerService(CarShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class
            => await this.db
                .Brands
                .OrderBy(a => a.Name)
                .ProjectTo<TModel>()
                .ToListAsync();

        public async Task<IEnumerable<TModel>> AllDealersAsync<TModel>() where TModel : class
            => await this.db
                .Dealers
                .OrderBy(a => a.Name)
                .ProjectTo<TModel>()
                .ToListAsync();

        public async Task CreateAsync(
            string name, string description, int brandId)
        {
            var artist = new Dealer
            {
                Name = name,
                Description = description,
                BrandId = brandId
            };

            await this.db.Dealers.AddAsync(artist);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await this.db.Dealers.AnyAsync(a => a.Id == id);

        public async Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class
            => await this.db
                .Dealers
                .Where(a => a.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();

        public async Task RemoveAsync(int id)
        {
            var artist = this.db.Dealers.Find(id);
            if (artist == null)
            {
                return;
            }

            this.db.Remove(artist);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, string name, string description, int brandId)
        {
            var car = this.db.Dealers.Find(id);
            if (car == null)
            {
                return;
            }

            var isChanged = false;
            if (car.Name != name)
            {
                car.Name = name;
                isChanged = true;
            }

            if (car.Description != description)
            {
                car.Description = description;
                isChanged = true;
            }

            if (car.BrandId != brandId)
            {
                car.BrandId = brandId;
                isChanged = true;
            }

            if (isChanged)
            {
                this.db.Dealers.Update(car);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
