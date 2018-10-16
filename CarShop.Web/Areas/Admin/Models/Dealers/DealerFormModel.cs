namespace CarShop.Web.Areas.Admin.Models.Dealers
{
    using CarShop.Data;
    using CarShop.Data.EntityModels;
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DealerFormModel
    {
        [Required]
        [MaxLength(DataConstants.DealerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.DealerDescriptionMaxLength)]
        public string Description { get; set; }

        [Display(Name = "Brands")]
        public int BrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }
    }
}
