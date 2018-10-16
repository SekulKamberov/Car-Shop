namespace CarShop.Services.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCart
    {
        private readonly IList<CartItem> items;

        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }

        public void AddToCart(int recordingId, int formatId)
        {
            var cartItem = this.GetCartItem(recordingId, formatId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CarId = recordingId,
                    ExtraId = formatId,
                    Quantity = 1
                };

                this.items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
        }

        public void ClearCartItems()
            => this.items.Clear();

        public IEnumerable<CartItem> GetItems
            => new List<CartItem>(this.items);

        public void DecreaseQuantity(int recordingId, int formatId)
        {
            var cartItem = this.GetCartItem(recordingId, formatId);
            if (cartItem != null && cartItem.Quantity > 0)
            {
                cartItem.Quantity--;

                if (cartItem.Quantity == 0)
                {
                    this.RemoveFromCart(recordingId, formatId);
                }
            }
        }

        public void IncreaseQuantity(int recordingId, int formatId)
        {
            var cartItem = this.GetCartItem(recordingId, formatId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
        }

        public void RemoveFromCart(int recordingId, int formatId)
        {
            var cartItem = this.GetCartItem(recordingId, formatId);
            if (cartItem != null)
            {
                this.items.Remove(cartItem);
            }
        }

        private CartItem GetCartItem(int recordingId, int formatId)
            => this.items.FirstOrDefault(i => i.CarId == recordingId
                                           && i.ExtraId == formatId);
    }
}
