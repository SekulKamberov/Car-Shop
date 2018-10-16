namespace CarShop.Web.Areas.Admin.Models.Cars
{
    using CarShop.Data;
    using CarShop.Data.EntityModels;
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CarFormModel
    {
        [Required]
        [MaxLength(DataConstants.CarTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.CarDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Length (in minutes)")]
        public int Length { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }

        [Display(Name = "Engine")]
        public Engine Engine { get; set; }

        [Display(Name = "Transmission")]
        public Transmission Transmission { get; set; }

        [Display(Name = "Fuel Type")]
        public FuelType FuelType { get; set; }

        [Display(Name = "Car Type")]
        public CarType CarType { get; set; }
    }
}
