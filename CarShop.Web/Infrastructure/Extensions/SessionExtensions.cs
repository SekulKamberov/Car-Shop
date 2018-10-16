namespace CarShop.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;
    using System;

    public static class SessionExtensions
    {
        private const string ShoppingCartKey = "Shopping_Cart_Id";

        public static string GetShoppingCartId(this ISession session)
        {
            var shoppingCartId = session.GetString(ShoppingCartKey);
            if (shoppingCartId == null)
            {
                shoppingCartId = Guid.NewGuid().ToString();
                session.SetString(ShoppingCartKey, shoppingCartId);
            }

            return shoppingCartId;
        }
    }
}
