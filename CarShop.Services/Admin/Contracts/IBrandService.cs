namespace CarShop.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBrandService
    {
        Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class;

        Task CreateAsync(string name,  string description);

        Task<bool> ExistsAsync(int id);

        Task<bool> ExistsStringAsync(string name);

        Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class;

        Task RemoveAsync(int id);

        Task UpdateAsync(int id, string name, string description);
    }
}
