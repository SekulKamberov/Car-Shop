namespace CarShop.Services.Admin.Models.Users
{
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
