namespace CarShop.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarShop.Data;
    using CarShop.Data.EntityModels;

    public interface IExtraService
    {
        Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class;

        Task<bool> CreateAsync(string name, string description);

        Task<bool> ExistsAsync(int id);

        Task<bool> ExistsAsync(string name);

        Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class;

        Task RemoveAsync(int id);

        Task<bool> UpdateAsync(int id, string name, string description);
    }
}
