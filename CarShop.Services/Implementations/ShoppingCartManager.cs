namespace CarShop.Services.Implementations
{
    using Models;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> shoppingCarts;  

        public ShoppingCartManager()
        {
            this.shoppingCarts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string cartId, int carId, int extraId)
            => this.GetShoppingCart(cartId).AddToCart(carId, extraId);

        public void Clear(string cartId)
            => this.GetShoppingCart(cartId).ClearCartItems();

        public void DecreaseQuantity(string cartId, int carId, int extraId)
            => this.GetShoppingCart(cartId).DecreaseQuantity(carId, extraId);

        public IEnumerable<CartItem> GetItems(string cartId)
            => new List<CartItem>(this.GetShoppingCart(cartId).GetItems);

        private ShoppingCart GetShoppingCart(string cartId)
            => this.shoppingCarts.GetOrAdd(cartId, new ShoppingCart());

        public void IncreaseQuantity(string cartId, int carId, int extraId)
            => this.GetShoppingCart(cartId).IncreaseQuantity(carId, extraId);

        public void RemoveFromCart(string cartId, int carId, int extraId)
            => this.GetShoppingCart(cartId).RemoveFromCart(carId, extraId);
    }
}
