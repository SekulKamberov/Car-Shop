namespace CarShop.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using CarShop.Services;

    public class CarsController : Controller
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await this.carService.Exists(id))
            {
                this.TempData.AddErrorMessage(WebConstants.NotFound);
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var carDetails = await this.carService.Details(id);

            return this.View(carDetails);
        }
    }
}
