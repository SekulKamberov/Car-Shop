namespace CarShop.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;

    public class CarDealerListingServiceModel : IMapFrom<CarDealer>, IHaveCustomMapping
    {
        public int CarId { get; set; }

        public string DealerName { get; set; }

        public string BrandName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<CarDealer, CarDealerListingServiceModel>()
                .ForMember(ra => ra.DealerName, cfg => cfg.MapFrom(ra => ra.Dealer.Name))
                .ForMember(ra => ra.BrandName, cfg => cfg.MapFrom(ra => ra.Dealer.Brand.Name));
        }
    }
}
