namespace CarShop.Web.Areas.Admin.Controllers
{
    using CarShop.Services.Admin.Models.Brands;
    using CarShop.Web.Areas.Admin.Models.Brands;
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;
    using CarShop.Services.Admin.Contracts;


    public class BrandsController : BaseAdminController
    {
        private readonly IBrandService brandService;

        public BrandsController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var brandData = await this.brandService
                .AllAsync<AdminBrandListingServiceModel>();

            return this.View(brandData);
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(BrandFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            bool exists = await this.brandService.ExistsStringAsync(model.Name);
            if (!exists)
            {
                await this.brandService.CreateAsync(model.Name, model.Description);
                this.TempData.AddSuccessMessage(string.Format(WebAdminConstants.BrandCreatedMsg, model.Name.ToStrongHtml()));
                return this.RedirectToAction(nameof(Index));
            }

            this.TempData.AddErrorMessage(WebAdminConstants.BrandExists);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brandData = await this.brandService.GetByIdAsync<AdminBrandDetailsToModifyServiceModel>(id);

            if (brandData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.BrandNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(new BrandFormModel
            {
                Name = brandData.Name,
                Description = brandData.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BrandFormModel model)
        {
            var brandExists = await this.brandService.ExistsAsync(id);
            if (!brandExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.BrandNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.TempData.AddSuccessMessage(string.Format(WebAdminConstants.BrandUpdatedMsg, model.Name.ToStrongHtml()));
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brandData = await this.brandService.GetByIdAsync<AdminBrandBasicServiceModel>(id);

            if (brandData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.BrandNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(brandData);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var brandData = await this.brandService.GetByIdAsync<AdminBrandBasicServiceModel>(id);

            if (brandData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.DealerNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            this.TempData.AddSuccessMessage(string.Format(WebAdminConstants.BrandDeletedMsg, brandData.Name.ToStrongHtml()));
            await this.brandService.RemoveAsync(id);
            return this.RedirectToAction(nameof(Index));
        }
    }
}
