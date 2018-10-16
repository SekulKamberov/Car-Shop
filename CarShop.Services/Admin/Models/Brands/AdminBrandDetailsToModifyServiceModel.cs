namespace CarShop.Services.Admin.Models.Brands
{
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminBrandDetailsToModifyServiceModel : IMapFrom<Brand>
    {
        public string Name { get; set; }

        public string CarModel { get; set; }

        public string Description { get; set; }
    }
}
