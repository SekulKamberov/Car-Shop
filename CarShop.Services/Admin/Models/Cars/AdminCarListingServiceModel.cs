namespace CarShop.Services.Admin.Models.Cars
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;
    using System;

    public class AdminCarListingServiceModel : IMapFrom<Car>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string BrandName { get; set; }

        public DateTime ReleaseDate { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Car, AdminCarListingServiceModel>()
                .ForMember(r => r.BrandName, cfg => cfg.MapFrom(r => r.Brand.Name));
        }
    }
}
