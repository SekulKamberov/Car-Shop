namespace CarShop.Data.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class OrderItem
    {
        public int Id { get; set; }

        public int CarId { get; set; } 

        public int ExtraId { get; set; }

        public string CarTitle { get; set; } 

        public string ExtraName { get; set; }

        public string Brand { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
