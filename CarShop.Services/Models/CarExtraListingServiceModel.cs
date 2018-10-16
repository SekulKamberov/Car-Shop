namespace CarShop.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;

    public class CarExtraListingServiceModel : IMapFrom<CarData>, IHaveCustomMapping
    {
        public int ExtraId { get; set; }

        public string ExtraName { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }

        public int Quantity { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<CarData, CarExtraListingServiceModel>()
                .ForMember(rf => rf.ExtraName, cfg => cfg.MapFrom(rf => rf.Extra.Name));
        }
    }
}
