namespace CarShop.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CarShop.Data.EntityModels;
    using Models;

    public interface ICarService
    {
        Task<IEnumerable<CartItemWithDetailsServiceModel>> GetCarsAsync();

        Task<IEnumerable<CartItemWithDetailsServiceModel>> CarTypeSearch(int id);

        Task<IEnumerable<CartItemWithDetailsServiceModel>> CarModelSearch(string search);

        Task<CarDetailsServiceModel> Details(int id);

        Task<bool> Exists(int id);
    }
}
