namespace CarShop.Services.Admin.Models.Cars
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;
    using Services.Admin.Models.Extras;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminCarListingWithExtrasServiceModel : IMapFrom<Car>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string BrandName { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IEnumerable<AdminExtraPriceQuantityServiceModel> AvailableExtras { get; set; }
            = new List<AdminExtraPriceQuantityServiceModel>(); // not mapped, received with an additional query

        public void ConfigureMapping(Profile mapper)
        {
            // NB! Mapping Formats (Id & Name) along with Recording data
            //var id = default(int);

            mapper
                .CreateMap<Car, AdminCarListingWithExtrasServiceModel>()
                //.ForMember(r => r.AvailableExtras, cfg => cfg
                //    .MapFrom(r => r.Extras
                //                   .Where(a => a.CarId == id)
                //                   .Select(a => a.Extra)))
                .ForMember(r => r.BrandName, cfg => cfg
                    .MapFrom(r => r.Brand.Name));
        }
    }
}
