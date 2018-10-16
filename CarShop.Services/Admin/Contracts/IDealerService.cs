namespace CarShop.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Data.EntityModels;

    public interface IDealerService
    {
        Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class;

        Task<IEnumerable<TModel>> AllDealersAsync<TModel>() where TModel : class;

        Task CreateAsync(string name, string description, int brandId);

        Task<bool> ExistsAsync(int id);

        Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class;

        Task RemoveAsync(int id);

        Task UpdateAsync(int id, string name, string description, int brandId);
    }
}
