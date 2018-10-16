namespace CarShop.Services.Admin.Models.Dealers
{
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminDealerDetailsToModifyServiceModel : IMapFrom<Dealer>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int BrandId { get; set; }
    }
}
