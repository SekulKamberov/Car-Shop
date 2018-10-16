namespace CarShop.Services.Admin.Models.Cars
{
    using Common.Mapping;
    using Data.EntityModels;
    using System;

    public class AdminCarDetailsToModifyServiceModel : IMapFrom<Car>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Length { get; set; }

        public string ImageUrl { get; set; }

        public int BrandId { get; set; }
    }
}
