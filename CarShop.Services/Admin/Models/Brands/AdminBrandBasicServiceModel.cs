namespace CarShop.Services.Admin.Models.Brands
{
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminBrandBasicServiceModel : IMapFrom<Brand>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
