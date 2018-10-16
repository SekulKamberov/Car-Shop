namespace CarShop.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IShoppingCartManager
    {
        void AddToCart(string cartId, int carId, int extraId);

        IEnumerable<CartItem> GetItems(string cartId);

        void RemoveFromCart(string cartId, int carId, int extraId);

        void IncreaseQuantity(string cartId, int carId, int extraId);

        void DecreaseQuantity(string cartId, int carId, int extraId);

        void Clear(string cartId);
    }
}
