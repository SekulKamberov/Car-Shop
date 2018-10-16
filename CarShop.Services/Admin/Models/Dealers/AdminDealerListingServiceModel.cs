namespace CarShop.Services.Admin.Models.Dealers
{
    using AutoMapper;
    using CarShop.Common.Mapping;
    using CarShop.Data.EntityModels;

    public class AdminDealerListingServiceModel : IMapFrom<Dealer>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Dealer, AdminDealerListingServiceModel>()
                .ForMember(a => a.Brand, cfg => cfg
                    .MapFrom(a => a.Brand.Name));
        }
    }
}
