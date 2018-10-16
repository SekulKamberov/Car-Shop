namespace CarShop.Services.Admin.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Data.EntityModels;

    using CarShop.Services.Admin.Models.Extras;
    using CarShop.Services.Admin.Models.Cars;

    using CarShop.Web.Data;
    using CarShop.Services.Admin.Contracts;


    public class CarService : ICarService
    {
        private readonly CarShopDbContext db;

        public CarService(CarShopDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddDealerAsync(int carId, int dealerId)
        {
            var Car = this.db.Cars.Find(carId);
            var Dealer = this.db.Dealers.Find(dealerId);
            var CarDealer = this.db.CarDealers.Find(carId, dealerId);

            if (Car == null
                || Dealer == null
                || CarDealer != null)
            {
                return false;
            }

            Car.Dealers.Add(new CarDealer
            {
                CarId = carId,
                DealerId = dealerId
            });

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddExtraAsync(int id, int extraId, decimal price, double discount, int quantity)
        {
            var Car = this.db.Cars.Find(id);
            var extra = this.db.Extras.Find(extraId);
            var CarExtra = this.db.CarInfo.Find(id, extraId);

            if (Car == null || extra == null || CarExtra != null)
            {
                return false;
            }

            Car.CarInfos.Add(new CarData
            {
                CarId = id,
                ExtraId = extraId,
                Price = price,
                Discount = discount,
                Quantity = quantity
            });

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class
            => await this.db
                .Cars
                .OrderByDescending(r => r.ReleaseDate)
                .ThenBy(r => r.Title)
                .ProjectTo<TModel>()
                .ToListAsync();

        public async Task CreateAsync( string title, string description, DateTime releaseDate, 
            int length, string imageUrl, int brandId, Engine Engine, Transmission Transmission, FuelType FuelType, CarType CarType)
        {
            var brandExists = this.db.Brands.Any(l => l.Id == brandId);
            if (!brandExists)
            {
                return;
            }

            var Car = new Car
            {
                Title = title,
                Description = description,
                ReleaseDate = releaseDate,
                Length = length,
                ImageUrl = imageUrl,
                BrandId = brandId,
                Engine = Engine,
                Transmission = Transmission,
                FuelType = FuelType,
                CarType = CarType
            };

            await this.db.Cars.AddAsync(Car);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await this.db.Cars.AnyAsync(r => r.Id == id);

        public async Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class
            => await this.db
                .Cars
                .Where(r => r.Id == id)
                .ProjectTo<TModel>(new { id })
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<AdminExtraPriceQuantityServiceModel>> GetExtrasAsync(int id)
        {
            if (!await this.ExistsAsync(id))
            {
                return null;
            }

            return await this.db
                .Cars
                .Where(r => r.Id == id)
                .Select(r => r
                    .CarInfos
                    .Select(rf => new AdminExtraPriceQuantityServiceModel
                    {
                        Id = rf.CarId,
                        Name = rf.Car.Title,
                        Price = rf.Price,
                        Discount = rf.Discount,
                        Quantity = rf.Quantity
                    })
                    .ToList())
                .FirstOrDefaultAsync();
        }

        public async Task<AdminExtraPriceQuantityServiceModel> GetPricingAsync(int id, int extraId)
        {
            var CarExtraExists = this.db
                .CarInfo.Any(rf => rf.CarId == id && rf.ExtraId == extraId);

            if (!CarExtraExists)
            {
                return null;
            }

            return await this.db.CarInfo.Where(rf => rf.CarId == id && rf.ExtraId == extraId)
                .ProjectTo<AdminExtraPriceQuantityServiceModel>().FirstOrDefaultAsync();
        }

        public async Task<AdminCarExtraNamesServiceModel> GetCarExtraNames(int id, int extraId)
        {
            var CarExtra = this.db.CarInfo.Find(id, extraId);
            if (CarExtra == null)
            {
                return null;
            }

            return await this.db
                .CarInfo
                .Where(rf => rf.CarId == id
                          && rf.ExtraId == extraId)
                .ProjectTo<AdminCarExtraNamesServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task RemoveDealerAsync(int id, int DealerId)
        {
            var CarDealer = this.db.CarDealers.Find(id, DealerId);
            if (CarDealer == null)
            {
                return;
            }

            this.db.Remove(CarDealer);
            await this.db.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var Car = this.db.Cars.Find(id);
            if (Car == null)
            {
                return;
            }

            this.db.Remove(Car);
            await this.db.SaveChangesAsync();
        }

        public async Task RemoveExtraAsync(int id, int extraId)
        {
            var CarExtra = this.db.CarInfo.Find(id, extraId);
            if (CarExtra == null)
            {
                return;
            }

            this.db.Remove(CarExtra);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(
            int id,
            string title, string description, DateTime releaseDate, int length, string imageUrl, int brandId, CarType CarType)
        {
            var Car = this.db.Cars.Find(id);
            if (Car == null)
            {
                return;
            }

            var brandExists = this.db.Extras.Any(l => l.Id == brandId);
            if (!brandExists)
            {
                return;
            }

            var isChanged = false;
            if (Car.Title != title)
            {
                Car.Title = title;
                isChanged = true;
            }

            if (Car.Description != description)
            {
                Car.Description = description;
                isChanged = true;
            }

            if (Car.ReleaseDate != releaseDate)
            {
                Car.ReleaseDate = releaseDate;
                isChanged = true;
            }

            if (Car.Length != length)
            {
                Car.Length = length;
                isChanged = true;
            }

            if (Car.ImageUrl != imageUrl)
            {
                Car.ImageUrl = imageUrl;
                isChanged = true;
            }

            if (Car.BrandId != brandId)
            {
                Car.BrandId = brandId;
                isChanged = true;
            }

            if (Car.CarType != CarType)
            {
                Car.CarType = CarType;
                isChanged = true;
            }

            if (isChanged)
            {
                this.db.Cars.Update(Car);
                await this.db.SaveChangesAsync();
            }
        }

        public async Task UpdateExtraPricing(int id, int extraId, decimal price, double discount, int quantity)
        {
            var CarExtra = this.db.CarInfo.Find(id, extraId);
            if (CarExtra == null)
            {
                return;
            }

            var isChanged = false;
            if (CarExtra.Price != price)
            {
                CarExtra.Price = price;
                isChanged = true;
            }

            if (CarExtra.Discount != discount)
            {
                CarExtra.Discount = discount;
                isChanged = true;
            }

            if (CarExtra.Quantity != quantity)
            {
                CarExtra.Quantity = quantity;
                isChanged = true;
            }

            if (isChanged)
            {
                this.db.CarInfo.Update(CarExtra);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
