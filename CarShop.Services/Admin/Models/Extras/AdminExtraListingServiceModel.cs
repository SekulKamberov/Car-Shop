namespace CarShop.Services.Admin.Models.Extras
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminExtraListingServiceModel //: IMapFrom<Format>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public string Gearbox { get; set; }

        public string Description { get; set; }

        //public void ConfigureMapping(Profile mapper)
        //{
        //    mapper
        //        .CreateMap<Format, AdminFormatListingServiceModel>()
        //        .ForMember(a => a.Gearbox, cfg => cfg
        //            .MapFrom(a => a.Transmission.ToString()));
        //}
    }
}
