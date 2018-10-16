namespace CarShop.Services.Admin.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarShop.Data.EntityModels;
    using Services.Admin.Models.Extras;
    using Services.Admin.Models.Cars;

    public interface ICarService
    {
        Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class;

        Task CreateAsync(
            string title, string description, DateTime releaseDate,
            int length, string imageUrl, int labelId, Engine Engine, Transmission Transmission, FuelType FuelType, CarType CarType);

        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<AdminExtraPriceQuantityServiceModel>> GetExtrasAsync(int id);

        Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class;

        Task<AdminExtraPriceQuantityServiceModel> GetPricingAsync(int id, int extraId);

        Task<AdminCarExtraNamesServiceModel> GetCarExtraNames(int id, int extraId);

        Task RemoveAsync(int id);

        Task<bool> AddDealerAsync(int id, int DealerId);

        Task<bool> AddExtraAsync(int id, int extraId, decimal price, double discount, int quantity);

        Task RemoveDealerAsync(int id, int DealerId);

        Task RemoveExtraAsync(int id, int extraId);

        Task UpdateAsync(int id,
            string title, string description, DateTime releaseDate,
            int length, string imageUrl, int labelId, CarType CarType);

        Task UpdateExtraPricing(int id, int extraId, decimal price, double discount, int quantity);
    }
}
