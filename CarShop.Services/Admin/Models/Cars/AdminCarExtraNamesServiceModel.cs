namespace CarShop.Services.Admin.Models.Cars
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminCarExtraNamesServiceModel : IMapFrom<CarData>, IHaveCustomMapping
    {
        public string CarTitle { get; set; }

        public string ExtraName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<CarData, AdminCarExtraNamesServiceModel>()
                .ForMember(rf => rf.CarTitle, cfg => cfg.MapFrom(rf => rf.Car.Title))
                .ForMember(rf => rf.ExtraName, cfg => cfg.MapFrom(rf => rf.Extra.Name));
        }
    }
}
