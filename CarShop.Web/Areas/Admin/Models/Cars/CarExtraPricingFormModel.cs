namespace CarShop.Web.Areas.Admin.Models.Cars
{
    using CarShop.Data;
    using Data;
    using System.ComponentModel.DataAnnotations;

    public class CarExtraPricingFormModel
    {
        public int Id { get; set; }

        public int ExtraId { get; set; }

        [Range(DataConstants.CarExtraMinQuantity, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(DataConstants.CarExtraMinPrice, double.MaxValue)]
        public decimal Price { get; set; }

        // In %
        [Range(DataConstants.CarExtraMinDiscount, DataConstants.CarExtraMaxDiscount)]
        public double Discount { get; set; }
    }
}
