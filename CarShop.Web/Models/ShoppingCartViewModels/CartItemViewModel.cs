namespace CarShop.Web.Models.ShoppingCartViewModels
{
    using AutoMapper;
    using CarShop.Data.EntityModels;
    using Common.Mapping;

    public class CartItemViewModel : IMapFrom<CarData>, IHaveCustomMapping
    {
        public int CarId { get; set; }

        public string CarTitle { get; set; }

        public int ExtraId { get; set; }

        public string ExtraName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }

        public string Image { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<CarData, CartItemViewModel>()
                .ForMember(rf => rf.CarTitle, cfg => cfg.MapFrom(rf => rf.Car.Title))
                .ForMember(rf => rf.ExtraName, cfg => cfg.MapFrom(rf => rf.Extra.Name))
                .ForMember(rf => rf.Image, cfg => cfg.MapFrom(rf => rf.Car.ImageUrl));
        }
    }
}
