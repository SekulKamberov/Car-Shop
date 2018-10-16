namespace CarShop.Data.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class CarData 
    {
        public int CarId { get; set; }

        public Car Car { get; set; }

        public int ExtraId { get; set; }

        public Extra Extra { get; set; }

        [Range(DataConstants.CarExtraMinQuantity, DataConstants.CarExtraMaxQuantity)]
        public int Quantity { get; set; }

        [Range(DataConstants.CarExtraMinPrice, DataConstants.CarExtraMaxPrice)]
        public decimal Price { get; set; }

        // In %
        [Range(DataConstants.CarExtraMinDiscount, DataConstants.CarExtraMaxDiscount)]
        public double Discount { get; set; }
    }
}
