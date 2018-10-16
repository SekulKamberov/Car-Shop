namespace CarShop.Web.Areas.Admin.Controllers
{
    using Admin.Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin;
    using Services.Admin.Models.Extras;
    using Services.Admin.Models.Brands;
    using Services.Admin.Models.Cars;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;
    using CarShop.Services.Admin.Models.Dealers;
    using CarShop.Services.Admin.Contracts;


    public class CarsController : BaseAdminController
    {
        private readonly ICarService carService;
        private readonly IBrandService brandService;
        private readonly IDealerService dealersService;
        private readonly IExtraService extraService;

        public CarsController(
            ICarService carService,
            IBrandService brandService,
            IDealerService dealersService,
            IExtraService extraService)
        {
            this.carService = carService;
            this.brandService = brandService;
            this.dealersService = dealersService;
            this.extraService = extraService;
        }

        public async Task<IActionResult> Index()
        {
            var CarsData = await this.carService
                .AllAsync<AdminCarListingServiceModel>();

            return this.View(CarsData);
        }

        public async Task<IActionResult> Dealers(int id)
        {
            var CarWithDealersData = await this.carService
                .GetByIdAsync<AdminCarListingWithDealersServiceModel>(id);

            var dealersSelectList =
                (await this.dealersService.AllDealersAsync<AdminDealerBasicServiceModel>())
                .Select(a => new SelectListItem
                {
                    //Text = $"{a.Name} ({a.CarBrand})",
                    Text = a.Dealer,
                    Value = a.DealerId.ToString()
                })
                .OrderBy(a => a.Text)
                .ToList();

            return this.View(new CarDealersListingModel
            {
                CarDealers = CarWithDealersData,
                Dealers = dealersSelectList
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDealerFromCar(int id, int DealerId)
        {
            var CarExists = await this.carService.ExistsAsync(id);
            if (!CarExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.CarNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var DealerExists = await this.dealersService.ExistsAsync(DealerId);
            if (!DealerExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.DealerNotFoundMsg);
                return this.RedirectToAction(nameof(Dealers), new { id });
            }

            var DealerData = await this.dealersService
                            .GetByIdAsync<AdminDealerBasicServiceModel>(DealerId);

            await this.carService.RemoveDealerAsync(id, DealerId);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.DealerRemovedFromCarMsg,
                DealerData.CarBrand.ToStrongHtml(),
                DealerData.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Dealers), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> AddDealerToCar(AddDealerToCarModel model)
        {
            var CarExists = await this.carService.ExistsAsync(model.CarId);
            if (!CarExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.CarNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var DealerExists = await this.dealersService.ExistsAsync(model.DealerId);
            if (!DealerExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.DealerNotFoundMsg);
                return this.RedirectToAction(nameof(Dealers), new { id = model.CarId });
            }

            if (!this.ModelState.IsValid)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.DealerInvalidDataMsg);
                return this.RedirectToAction(nameof(Dealers), new { id = model.CarId });
            }

            var success = await this.carService.AddDealerAsync(
                model.CarId,
                model.DealerId);

            var DealerData = await this.dealersService
                            .GetByIdAsync<AdminDealerBasicServiceModel>(model.DealerId);

            if (!success)
            {
                this.TempData.AddErrorMessage(string.Format(
                    WebAdminConstants.DealerAlreadyContributingToCarMsg,
                    DealerData.CarBrand.ToStrongHtml(),
                    DealerData.Name.ToStrongHtml()));

                return this.RedirectToAction(nameof(Dealers), new { id = model.CarId });
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.DealerAddedToCarMsg,
                DealerData.CarBrand.ToStrongHtml(),
                DealerData.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Dealers), new { id = model.CarId });
        }

        public async Task<IActionResult> Extras(int id)
        {
            var CarWithExtrasData = await this.carService
                .GetByIdAsync<AdminCarListingWithExtrasServiceModel>(id);

            CarWithExtrasData.AvailableExtras = await this.carService
                .GetExtrasAsync(id);

            var extrasSelectList =
                (await this.extraService.AllAsync<AdminExtraBasicServiceModel>())
                .Select(f => new SelectListItem
                {
                    Text = f.Name,
                    Value = f.Id.ToString()
                })
                .OrderBy(a => a.Text)
                .ToList();

            return this.View(new CarExtrasListingModel
            {
                Car = CarWithExtrasData,
                Extras = extrasSelectList
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveExtraFromCar(int id, int ExtraId)
        {
            var CarExists = await this.carService.ExistsAsync(id);
            if (!CarExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.CarNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var extraExists = await this.extraService.ExistsAsync(ExtraId);
            if (!extraExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ExtraNotFoundMsg);
                return this.RedirectToAction(nameof(Extras), new { id });
            }

            var extraData = await this.extraService
                            .GetByIdAsync<AdminExtraBasicServiceModel>(ExtraId);

            await this.carService.RemoveExtraAsync(id, ExtraId);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.ExtraRemovedFromCarMsg,
                extraData.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Extras), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> AddExtraToCar(AddExtraCarModel model)
        {
            var CarExists = await this.carService.ExistsAsync(model.CarId);
            if (!CarExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.CarNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var extraExists = await this.extraService.ExistsAsync(model.ExtraId);
            if (!extraExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ExtraNotFoundMsg);
                return this.RedirectToAction(nameof(Extras), new { id = model.CarId });
            }

            if (!this.ModelState.IsValid)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ExtraInvalidDataMsg);
                return this.RedirectToAction(nameof(Extras), new { id = model.CarId });
            }

            var success = await this.carService.AddExtraAsync(
                model.CarId,
                model.ExtraId,
                model.Price,
                model.Discount,
                model.Quantity);

            var extraData = await this.extraService.GetByIdAsync<AdminExtraBasicServiceModel>(model.ExtraId);

            if (!success)
            {
                this.TempData.AddErrorMessage(string.Format(
                    WebAdminConstants.ExtraAlreadyAddedToCarMsg,
                    extraData.Name.ToStrongHtml()));

                return this.RedirectToAction(nameof(Extras), new { id = model.CarId });
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.ExtraAddedToCarMsg,
                extraData.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Extras), new { id = model.CarId });
        }

        public async Task<IActionResult> Pricing(int id, int ExtraId)
        {
            var CarExists = await this.carService.ExistsAsync(id);
            if (!CarExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.CarNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var extraExists = await this.extraService.ExistsAsync(ExtraId);
            if (!extraExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ExtraNotFoundMsg);
                return this.RedirectToAction(nameof(Extras), new { id });
            }

            var CarExtraPricingData = await this.carService
                .GetPricingAsync(id, ExtraId);

            return this.View(new CarExtraPricingFormModel
            {
                Id = id,
                ExtraId = ExtraId,
                Price = CarExtraPricingData.Price,
                Discount = CarExtraPricingData.Discount,
                Quantity = CarExtraPricingData.Quantity
            });
        }

        [HttpPost]
        public async Task<IActionResult> Pricing(int id, CarExtraPricingFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var CarExists = await this.carService.ExistsAsync(id);
            if (!CarExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.CarNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var extraExists = await this.extraService.ExistsAsync(model.ExtraId);
            if (!extraExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ExtraNotFoundMsg);
                return this.RedirectToAction(nameof(Extras), new { id });
            }

            await this.carService.UpdateExtraPricing(
                id,
                model.ExtraId,
                model.Price,
                model.Discount,
                model.Quantity);

            var CarExtraData = await this.carService.GetCarExtraNames(id, model.ExtraId);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.PricingUpdatedMsg,
                CarExtraData.CarTitle.ToStrongHtml(),
                CarExtraData.ExtraName.ToStrongHtml()));

            return this.RedirectToAction(nameof(Extras), new { id });
        }

        public async Task<IActionResult> Create()
            => this.View(new CarFormModel
            {
                Brands = await this.GetBrandsSelectListAsync()
            });

        [HttpPost]
        public async Task<IActionResult> Create(CarFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Brands = await this.GetBrandsSelectListAsync();
                return this.View(model);
            }

            if (!await this.BrandExists(model.BrandId))
            {
                return this.BadRequest(WebAdminConstants.BrandNotFoundMsg);
            }

            await this.carService.CreateAsync(
                model.Title,
                model.Description,
                (DateTime)model.ReleaseDate,
                model.Length,
                model.ImageUrl,
                model.BrandId,
                model.Engine,
                model.Transmission,
                model.FuelType,
                model.CarType
                );

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.CarCreatedMsg,
                model.Title.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var CarData = await this.carService
                .GetByIdAsync<AdminCarDetailsToModifyServiceModel>(id);

            if (CarData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.CarNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(new CarFormModel
            {
                Title = CarData.Title,
                Description = CarData.Description,
                ReleaseDate = CarData.ReleaseDate,
                ImageUrl = CarData.ImageUrl,
                Length = CarData.Length,
                BrandId = CarData.BrandId,
                Brands = await this.GetBrandsSelectListAsync()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CarFormModel model)
        {
            var CarExists = await this.carService.ExistsAsync(id);
            if (!CarExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.CarNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            if (!await this.BrandExists(model.BrandId))
            {
                return this.BadRequest(WebAdminConstants.BrandNotFoundMsg);
            }

            if (!this.ModelState.IsValid)
            {
                model.Brands = await this.GetBrandsSelectListAsync();
                return this.View(model);
            }

            await this.carService.UpdateAsync(
                id,
                model.Title,
                model.Description,
                (DateTime)model.ReleaseDate,
                model.Length,
                model.ImageUrl,
                model.BrandId,
                model.CarType);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.CarUpdatedMsg,
                model.Title.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var CarData = await this.carService
                .GetByIdAsync<AdminCarListingServiceModel>(id);

            if (CarData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.CarNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(CarData);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var CarData = await this.carService
                .GetByIdAsync<AdminCarBasicServiceModel>(id);

            if (CarData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.CarNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            await this.carService.RemoveAsync(id);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.CarDeletedMsg,
                CarData.Title.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetBrandsSelectListAsync()
            => (await this.brandService.AllAsync<AdminBrandBasicServiceModel>())
                .Select(l => new SelectListItem
                {
                    Text = l.Name,
                    Value = l.Id.ToString()
                })
                .ToList();

        private async Task<bool> BrandExists(int brandId)
            => await this.brandService.ExistsAsync(brandId);
    }
}
