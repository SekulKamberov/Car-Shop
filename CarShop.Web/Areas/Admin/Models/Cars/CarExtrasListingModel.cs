namespace CarShop.Web.Areas.Admin.Models.Cars
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models.Cars;
    using System.Collections.Generic;

    public class CarExtrasListingModel
    {
        public AdminCarListingWithExtrasServiceModel Car { get; set; }

        public IEnumerable<SelectListItem> Extras { get; set; } = new List<SelectListItem>(); //Formats
    }
}
