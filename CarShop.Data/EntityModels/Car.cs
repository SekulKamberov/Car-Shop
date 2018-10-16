namespace CarShop.Data.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
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

        public Brand Brand { get; set; }

        public CarType CarType { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<CarDealer> Dealers { get; set; } = new List<CarDealer>();

        public ICollection<CarData> CarInfos { get; set; } = new List<CarData>();
    }
}
