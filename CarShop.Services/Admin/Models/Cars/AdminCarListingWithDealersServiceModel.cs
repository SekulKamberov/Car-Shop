namespace CarShop.Services.Admin.Models.Cars
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;
    using Models.Dealers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminCarListingWithDealersServiceModel : IMapFrom<Car>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string BrandName { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int CarId { get; set; }

        public IEnumerable<AdminDealerBasicServiceModel> ContibutingDealers { get; set; }//ContibutingArtists
            = new List<AdminDealerBasicServiceModel>();

        public void ConfigureMapping(Profile mapper)
        {
            var id = default(int);
             mapper
            .CreateMap<Car, AdminCarListingWithDealersServiceModel>()
            .ForMember(s => s.BrandName, ops => ops.MapFrom(r => r.Brand.Name))
            .ForMember(v => v.ContibutingDealers, ops => ops.MapFrom(r => r.Dealers.Where(a => a.CarId == id).Select(a => a.Dealer)))
            .ForMember(z => z.CarId, ops => ops.MapFrom(r => r.Id));
        }
    }
}
