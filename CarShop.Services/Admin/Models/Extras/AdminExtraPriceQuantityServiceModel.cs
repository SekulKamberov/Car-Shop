namespace CarShop.Services.Admin.Models.Extras
{
    using AutoMapper;
    using Common.Mapping;
    using Data.EntityModels;
    using System.ComponentModel.DataAnnotations;

    public class AdminExtraPriceQuantityServiceModel : IMapFrom<CarData>, IHaveCustomMapping
    {
        public int Id { get; set; }

        [Display(Name = "Extra")]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Discount, %")]
        public double Discount { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<CarData, AdminExtraPriceQuantityServiceModel>()
                .ForMember(rf => rf.Name, cfg => cfg.MapFrom(rf => rf.Extra.Name))
                .ForMember(rf => rf.Id, cfg => cfg.MapFrom(rf => rf.Extra.Id));
        }
    }
}
