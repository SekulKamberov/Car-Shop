namespace CarShop.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;
    using System;

    public class CartTypeWithDetailsServiceModel : CartItem, IMapFrom<CarData>, IHaveCustomMapping
    {
        public string CarTitle { get; set; }

        public string ExtraName { get; set; }

        public string BrandName { get; set; }

        public string ImageUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Engine { get; set; }

        public string Fuel { get; set; }

        public string CarType { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<CarData, CartItemWithDetailsServiceModel>()
                .ForMember(rf => rf.CarTitle, cfg => cfg.MapFrom(rf => rf.Car.Title))
                .ForMember(rf => rf.BrandName, cfg => cfg.MapFrom(rf => rf.Car.Brand.Name))
                .ForMember(rf => rf.ImageUrl, cfg => cfg.MapFrom(rf => rf.Car.ImageUrl))
                .ForMember(rf => rf.ReleaseDate, cfg => cfg.MapFrom(rf => rf.Car.ReleaseDate))
                .ForMember(rf => rf.ExtraName, cfg => cfg.MapFrom(rf => rf.Car.Title))
                .ForMember(rf => rf.Engine, cfg => cfg.MapFrom(rf => rf.Car.Engine.ToString()))
                .ForMember(rf => rf.Fuel, cfg => cfg.MapFrom(rf => rf.Car.FuelType.ToString()))
                .ForMember(rf => rf.CarType, cfg => cfg.MapFrom(rf => rf.Car.CarType.ToString()));
        }
    }
}
