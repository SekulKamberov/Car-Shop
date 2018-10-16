namespace CarShop.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Users;

    public interface IUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}
