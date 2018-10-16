namespace CarShop.Data.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime CompletionDate { get; set; } = DateTime.UtcNow;

        public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
