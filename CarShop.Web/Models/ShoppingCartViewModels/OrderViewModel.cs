namespace CarShop.Web.Models.ShoppingCartViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderViewModel
    {
        public IEnumerable<CartItemViewModel> Items { get; set; }

        //[Range(0.01, double.MaxValue)]
        public decimal OrderTotal { get; set; }
    }
}
