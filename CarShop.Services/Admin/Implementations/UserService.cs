namespace CarShop.Services.Admin.Implementations
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using AutoMapper.QueryableExtensions;

    using Data;
    using CarShop.Web.Data;
    using Admin.Models.Users;

    using CarShop.Services.Admin.Contracts;

    public class UserService : IUserService
    {
        private readonly CarShopDbContext db;

        public UserService(CarShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
                .Users
                .OrderBy(u => u.Name)
                .ThenBy(u => u.UserName)
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
    }
}
