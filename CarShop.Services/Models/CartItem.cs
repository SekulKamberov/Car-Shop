namespace CarShop.Services.Models
{
    using CarShop.Data.EntityModels;
    using Common.Mapping;

    public class CartItem : IMapFrom<CarData>
    {
        public int CarId { get; set; }

        public int ExtraId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }
    }
}
