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

    public class BrandService : IBrandService
    {
        private readonly CarShopDbContext db;

        public BrandService(CarShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class
            => await this.db.Brands.OrderBy(l => l.Name).ProjectTo<TModel>().ToListAsync();

        public async Task CreateAsync(string name, string description)
        {
            var exists = this.db.Brands.Any(a => a.Name == name);
            if (exists)
            {
                return;
            }
            var label = new Brand
            {
                Name = name,
                Description = description
            };

            await this.db.Brands.AddAsync(label);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await this.db.Brands.AnyAsync(l => l.Id == id);

        public async Task<bool> ExistsStringAsync(string name)
            => await this.db.Brands.AnyAsync(l => l.Name == name);

        public async Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class
             => await this.db.Brands.Where(l => l.Id == id).ProjectTo<TModel>().FirstOrDefaultAsync();

        public async Task RemoveAsync(int id)
        {
            var label = this.db.Brands.Find(id);
            if (label == null)
            {
                return;
            }

            this.db.Remove(label);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, string name, string description)
        {
            var label = this.db.Brands.Find(id);
            if (label == null)
            {
                return;
            }

            var isChanged = false;
            if (label.Name != name)
            {
                label.Name = name;
                isChanged = true;
            }

            if (label.Description != description)
            {
                label.Description = description;
                isChanged = true;
            }

            if (isChanged)
            {
                this.db.Brands.Update(label);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
