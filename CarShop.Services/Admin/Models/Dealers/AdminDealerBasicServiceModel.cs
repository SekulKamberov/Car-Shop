namespace CarShop.Services.Admin.Models.Dealers
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminDealerBasicServiceModel : IMapFrom<Dealer>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CarBrand { get; set; }

        public int DealerId { get; set; }

        public string Dealer { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Dealer, AdminDealerBasicServiceModel>()
                .ForMember(a => a.CarBrand, cfg => cfg.MapFrom(a => a.Brand.Name))
                .ForMember(a => a.Dealer, cfg => cfg.MapFrom(a => a.Name))
                .ForMember(a => a.DealerId, cfg => cfg.MapFrom(a => a.Id));
        }
    }
}
