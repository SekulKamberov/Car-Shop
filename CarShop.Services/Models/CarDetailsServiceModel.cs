namespace CarShop.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;
    using System;
    using System.Collections.Generic;

    public class CarDetailsServiceModel : IMapFrom<Car>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int Length { get; set; }

        public string ImageUrl { get; set; }

        public string BrandName { get; set; }

        public string Engine { get; set; }

        public string Transmission { get; set; }

        public string FuelType { get; set; }

        public IEnumerable<CarExtraListingServiceModel> Extras { get; set; } = new List<CarExtraListingServiceModel>();

        public IEnumerable<CarDealerListingServiceModel> Dealers { get; set; } = new List<CarDealerListingServiceModel>();

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Car, CarDetailsServiceModel>()
                .ForMember(r => r.BrandName, cfg => cfg.MapFrom(r => r.Brand.Name))
                .ForMember(r => r.Engine, cfg => cfg.MapFrom(r => r.Engine.ToString()))
                .ForMember(r => r.Transmission, cfg => cfg.MapFrom(r => r.Transmission.ToString()))
                .ForMember(r => r.FuelType, cfg => cfg.MapFrom(r => r.FuelType.ToString()));
        }
    }
}
