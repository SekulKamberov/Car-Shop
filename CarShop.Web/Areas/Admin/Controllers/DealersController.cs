namespace CarShop.Web.Areas.Admin.Controllers
{
    using Admin.Models.Dealers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin;
    using Services.Admin.Models.Dealers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;
    using CarShop.Services.Admin.Contracts;


    public class DealersController : BaseAdminController
    {
        private readonly IDealerService dealerService;

        public DealersController(IDealerService dealerService)
        {
            this.dealerService = dealerService;
        }

        public async Task<IActionResult> Index()
        {
            var dealersData = await this.dealerService
                .AllDealersAsync<AdminDealerListingServiceModel>();

            return this.View(dealersData);
        }

        public async Task<IActionResult> Create()
            => this.View(new DealerFormModel
            {
                Brands = await this.GetBrandsSelectListAsync()
            });

        [HttpPost]
        public async Task<IActionResult> Create(DealerFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.dealerService.CreateAsync(model.Name, model.Description, model.BrandId);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.DealerCreatedMsg,
                model.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dealersData = await this.dealerService
                .GetByIdAsync<AdminDealerDetailsToModifyServiceModel>(id);

            if (dealersData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.DealerNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(new DealerFormModel
            {
                Name = dealersData.Name,
                Description = dealersData.Description,
                Brands = await this.GetBrandsSelectListAsync()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DealerFormModel model)
        {
            var dealerExists = await this.dealerService.ExistsAsync(id);
            if (!dealerExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.DealerNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.dealerService.UpdateAsync(
                id,
                model.Name,
                model.Description,
                model.BrandId);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.DealerUpdatedMsg,
                model.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dealerData = await this.dealerService
                .GetByIdAsync<AdminDealerBasicServiceModel>(id);

            if (dealerData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.DealerNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(dealerData);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var dealerData = await this.dealerService
                .GetByIdAsync<AdminDealerBasicServiceModel>(id);

            if (dealerData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.DealerNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.DealerDeletedMsg,
                dealerData.Name.ToStrongHtml(),
                dealerData.CarBrand));

            await this.dealerService.RemoveAsync(id);

            return this.RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetBrandsSelectListAsync()
             => (await this.dealerService.AllAsync<AdminBrandsBasicServiceModel>())
                .Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                })
                .ToList();
    }
}
