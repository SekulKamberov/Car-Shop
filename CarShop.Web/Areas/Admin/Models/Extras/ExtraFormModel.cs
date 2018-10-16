namespace CarShop.Web.Areas.Admin.Models.Extras
{
    using System.ComponentModel.DataAnnotations;

    using Data;
    using CarShop.Data;
    using CarShop.Data.EntityModels;

    public class ExtraFormModel
    {
        [Required]
        [MaxLength(DataConstants.ExtraNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public Transmission Transmission { get; set; }

        [Required]
        [MaxLength(DataConstants.ExtraDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
