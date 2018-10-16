namespace CarShop.Data.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CarDealer
    { 
        public int CarId { get; set; }

        public Car Car { get; set; }

        public int DealerId { get; set; }

        public Dealer Dealer { get; set; }
    }
}
