namespace CarShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using CarShop.Web.Models;
    using CarShop.Services;
    using CarShop.Data.EntityModels;

    public class HomeController : Controller
    {
        private readonly ICarService carService;

        public HomeController(ICarService recordingService)
        {
            this.carService = recordingService;
        }

        public async Task<IActionResult> Index()
        {
            var recordingsData = await this.carService.GetCarsAsync();
            return View(recordingsData);
        }

        public async Task<IActionResult> ModelSearch(string search)
        {
            var carModel = await this.carService.CarModelSearch(search);
            if (carModel != null)
            {
                return View("Index", carModel);
            }
            else
            {
                return BadRequest("Sorry no results");
            }
        }

        public async Task<IActionResult> TypeSearch(int id)
        {
            var carType = await this.carService.CarTypeSearch(id);
            return View("Index", carType);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult More()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}
