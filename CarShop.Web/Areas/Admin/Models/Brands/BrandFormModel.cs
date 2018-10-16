namespace CarShop.Web.Areas.Admin.Models.Brands
{
    using CarShop.Data;
    using Data;
    using System.ComponentModel.DataAnnotations;

    public class BrandFormModel
    {
        [Required]
        [MaxLength(DataConstants.BrandNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.BrandDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
