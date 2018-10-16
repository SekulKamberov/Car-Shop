namespace CarShop.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using AutoMapper.QueryableExtensions;

    using Data;
    using CarShop.Web.Data;
    using Services.Models;
    using CarShop.Data.EntityModels;

    public class CarService : ICarService
    {
        private readonly CarShopDbContext db;

        public CarService(CarShopDbContext db)
        {
            this.db = db;
        }

        public async Task<CarDetailsServiceModel> Details(int id)
        {
            if (!await this.Exists(id))
            {
                return null;
            }

            return await this.db
                .Cars
                .Where(r => r.Id == id)
                .ProjectTo<CarDetailsServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Exists(int id)
            => await this.db.Cars.AnyAsync(r => r.Id == id);

        public async Task<IEnumerable<CartItemWithDetailsServiceModel>> GetCarsAsync()
            => await this.db
                .CarInfo
                .Where(rf => rf.Quantity > 0)
                .ProjectTo<CartItemWithDetailsServiceModel>()
                .OrderByDescending(rf => rf.ReleaseDate)
                .ThenBy(rf => rf.CarTitle)
                .ToListAsync();

        public async Task<IEnumerable<CartItemWithDetailsServiceModel>> CarTypeSearch(int id)
            => await this.db.CarInfo
                .Where(s => s.Car.CarType == (CarType)id)
                .ProjectTo<CartItemWithDetailsServiceModel>()
                .OrderByDescending(rf => rf.Price).ToListAsync();

        public async Task<IEnumerable<CartItemWithDetailsServiceModel>> CarModelSearch(string search)
            => await this.db.CarInfo
                .Where(s => s.Car.Title.ToLower().Contains(search.ToLower()))
                .ProjectTo<CartItemWithDetailsServiceModel>()
                .OrderByDescending(rf => rf.Price).ToListAsync();

    }
}
