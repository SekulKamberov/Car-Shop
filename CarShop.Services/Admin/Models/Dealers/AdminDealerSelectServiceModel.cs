namespace CarShop.Services.Admin.Models.Dealers
{
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminDealerSelectServiceModel : IMapFrom<Dealer>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
