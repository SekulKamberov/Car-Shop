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


    public class ExtraService : IExtraService
    {
        private readonly CarShopDbContext db;

        public ExtraService(CarShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class
            => await this.db
                .Extras
                .OrderBy(f => f.Name)
                .ProjectTo<TModel>()
                .ToListAsync();

        public async Task<bool> CreateAsync(string name, string description)
        {
            if (await this.ExistsAsync(name))
            {
                return false;
            }

            var format = new Extra
            {
                Name = name,
                Description = description
            };

            this.db.Extras.Add(format);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(int id)
            => await this.db.Extras.AnyAsync(f => f.Id == id);

        public async Task<bool> ExistsAsync(string name)
            => await this.db.Extras.AnyAsync(f => f.Name.ToLower() == name.ToLower());

        public async Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class
           => await this.db
               .Extras
               .Where(f => f.Id == id)
               .ProjectTo<TModel>()
               .FirstOrDefaultAsync();

        public async Task RemoveAsync(int id)
        {
            var format = this.db.Extras.Find(id);
            if (format == null)
            {
                return;
            }

            this.db.Remove(format);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, string name, string description)
        {
            var format = this.db.Extras.Find(id);
            if (format == null)
            {
                return false;
            }

            if (format.Name.ToLower() != name.ToLower()
                && await this.ExistsAsync(name))
            {
                return false;
            }

            var isChanged = false;
            if (format.Name != name)
            {
                format.Name = name;
                isChanged = true;
            }

            if (format.Description != description)
            {
                format.Description = description;
                isChanged = true;
            }

            if (isChanged)
            {
                this.db.Extras.Update(format);
                await this.db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
