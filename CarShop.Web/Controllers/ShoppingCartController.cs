namespace CarShop.Web.Controllers
{
    using AutoMapper.QueryableExtensions;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Data;
    using Services;
    using CarShop.Data.EntityModels;
    using CarShop.Web.Models;
    using Web.Infrastructure.Extensions;
    using CarShop.Web.Models.ShoppingCartViewModels;

    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly CarShopDbContext db;
        private readonly UserManager<User> userManager;

        public ShoppingCartController(
            IShoppingCartManager shoppingCartManager,
            CarShopDbContext db,
            UserManager<User> userManager)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.db = db;
            this.userManager = userManager;
        }

        public IActionResult AddToCart(int carId, int extraId)
        {
            var carFormat = this.db
                .CarInfo
                .Where(rf => rf.ExtraId == extraId
                        && rf.CarId == carId)
                .FirstOrDefault();

            if (carFormat == null)
            {
                this.TempData.AddErrorMessage(WebConstants.ExtraNotFound);
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (carFormat.Quantity == 0)
            {
                this.TempData.AddErrorMessage(WebConstants.NoQuantity);
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            this.shoppingCartManager.AddToCart(shoppingCartId, carId, extraId);

            return this.RedirectToAction(nameof(Items));
        }

        public IActionResult Items()
        {
            var itemsWithDetails = this.GetShoppingCartItems();

            return View(itemsWithDetails);
        }

        public IActionResult Remove(int recordingId, int formatId)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            this.shoppingCartManager.RemoveFromCart(shoppingCartId, recordingId, formatId);

            return this.RedirectToAction(nameof(Items));
        }

        public IActionResult IncreaseQuantity(int recordingId, int formatId)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            this.shoppingCartManager.IncreaseQuantity(shoppingCartId, recordingId, formatId);

            return this.RedirectToAction(nameof(Items));
        }

        public IActionResult DescreaseQuantity(int recordingId, int formatId)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            this.shoppingCartManager.DecreaseQuantity(shoppingCartId, recordingId, formatId);

            return this.RedirectToAction(nameof(Items));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> FinishOrder(decimal orderTotal)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            if (orderTotal <= 0)
            {
                this.TempData.AddErrorMessage(WebConstants.IsEmpty);

                this.shoppingCartManager.Clear(shoppingCartId);

                return this.RedirectToAction(nameof(Items));
            }

            var cartItems = this.GetShoppingCartItems();
            var order = new Order
            {
                UserId = this.userManager.GetUserId(this.User),
                OrderTotal = orderTotal
            };

            var orderItems = new List<OrderItem>();
            foreach (var item in cartItems)
            {
                orderItems.Add(new OrderItem
                {
                    CarId = item.CarId,
                    CarTitle = item.CarTitle,
                    ExtraId = item.ExtraId,
                    ExtraName = item.ExtraName,
                    Brand = this.db
                        .Cars
                        .Where(r => r.Id == item.CarId)
                        .Select(r => r.Brand.Name)
                        .FirstOrDefault(),
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Discount = item.Discount
                });

                // update remaining Qty in db
                var carFormat = this.db
                    .CarInfo
                    .Where(rf => rf.ExtraId == item.ExtraId
                              && rf.CarId == item.CarId)
                    .FirstOrDefault();
                carFormat.Quantity -= item.Quantity;
                this.db.CarInfo.Update(carFormat);
            }

            order.OrderItems = orderItems;

            if (orderTotal != order.OrderItems
                            .Sum(i => i.Quantity * i.Price * (1 - (decimal)i.Discount / 100)))
            {
                this.TempData.AddErrorMessage(WebConstants.NotValid);
                return this.RedirectToAction(nameof(Items));
            }

            await this.db.Orders.AddAsync(order);
            await this.db.SaveChangesAsync();

            this.TempData.AddSuccessMessage(WebConstants.Completed);

            this.shoppingCartManager.Clear(shoppingCartId);

            return this.RedirectToAction(nameof(Items));
        }

        private List<CartItemViewModel> GetShoppingCartItems()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemIds = items
                .Select(i => $"{i.CarId}:{i.ExtraId}")
                .ToList();

            var itemQuantitiesInStore = this.db
                .CarInfo
                .Where(rf => itemIds.Contains($"{rf.CarId}:{rf.ExtraId}"))
                .ToDictionary(i => $"{i.CarId}:{i.ExtraId}", i => i.Quantity);

            var itemQuantities = items.ToDictionary(i => $"{i.CarId}:{i.ExtraId}", i => i.Quantity);

            var itemsWithDetails = this.db
                .CarInfo
                .Where(rf => itemIds.Contains($"{rf.CarId}:{rf.ExtraId}"))  
                .ProjectTo<CartItemViewModel>()
                .ToList();

            itemsWithDetails
                .ForEach(i => i.Quantity = Math.Min(
                    itemQuantities[$"{i.CarId}:{i.ExtraId}"],
                    itemQuantitiesInStore[$"{i.CarId}:{i.ExtraId}"]));

            return itemsWithDetails;
        }
    }
}
