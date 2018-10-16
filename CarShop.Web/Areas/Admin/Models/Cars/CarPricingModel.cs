namespace CarShop.Web.Areas.Admin.Models.Cars
{
    using Services.Admin.Models.Extras;
    using Services.Admin.Models.Cars;
    using CarShop.Web.Areas.Admin.Models.Cars;

    public class CarPricingModel
    {
        public AdminCarListingServiceModel Car { get; set; }

        public CarExtraPricingFormModel ExtraPricing { get; set; }
    }
}
