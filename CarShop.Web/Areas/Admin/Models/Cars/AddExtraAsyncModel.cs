namespace CarShop.Web.Areas.Admin.Models.Cars
{
    using CarShop.Common.Mapping;
    using CarShop.Data;
    using CarShop.Data.EntityModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddExtraAsyncModel : IMapFrom<Car>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.CarTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.CarDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Range(DataConstants.CarMinLength, DataConstants.CarMaxLength)]
        public int Length { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public Engine Engine { get; set; }

        public Transmission Transmission { get; set; }

        public FuelType FuelType { get; set; }

        public int BrandId { get; set; }

        public CarType CarType { get; set; }
    }
}
