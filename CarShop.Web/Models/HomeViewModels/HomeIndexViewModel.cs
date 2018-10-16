namespace CarShop.Web.Models.HomeViewModels
{
    using CarShop.Services.Models;
    using System.Collections.Generic;

    public class HomeIndexViewModel : SearchFormModel
    {
        public IEnumerable<CartItemWithDetailsServiceModel> Cars { get; set; } = new List<CartItemWithDetailsServiceModel>();
    }
}
