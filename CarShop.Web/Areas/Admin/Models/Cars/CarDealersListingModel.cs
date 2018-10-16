namespace CarShop.Web.Areas.Admin.Models.Cars
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models.Cars;
    using System.Collections.Generic;

    public class CarDealersListingModel
    {
        public AdminCarListingWithDealersServiceModel CarDealers { get; set; }//Recording

        public IEnumerable<SelectListItem> Dealers { get; set; } = new List<SelectListItem>();//Artists
    }
}
