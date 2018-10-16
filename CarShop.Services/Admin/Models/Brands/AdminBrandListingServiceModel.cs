namespace CarShop.Services.Admin.Models.Brands
{
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminBrandListingServiceModel : IMapFrom<Brand>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
