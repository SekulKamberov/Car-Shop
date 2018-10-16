namespace CarShop.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Services.Admin;
    using Services.Admin.Models.Extras;
    using CarShop.Web.Areas.Admin.Models.Extras;
    using Web.Infrastructure.Extensions;
    using CarShop.Services.Admin.Models.Dealers;
    using CarShop.Services.Admin.Contracts;


    public class ExtrasController : BaseAdminController
    {
        private readonly IExtraService extraService;

        public ExtrasController(IExtraService extraService)
        {
            this.extraService = extraService;
        }

        public async Task<IActionResult> Index()
        {
            var extrasData = await this.extraService
                .AllAsync<AdminExtraListingServiceModel>();

            return this.View(extrasData);
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(ExtraFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var success = await this.extraService.CreateAsync(
                model.Name,
               // model.Transmission,
                model.Description);

            if (!success)
            {
                this.TempData.AddErrorMessage(string.Format(
                    WebAdminConstants.ExtraExistsMsg,
                    model.Name.ToStrongHtml()));

                return this.View(model);
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.ExtraCreatedMsg,
                model.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var extraData = await this.extraService
                .GetByIdAsync<AdminExtraDetailsToModifyServiceModel>(id);

            if (extraData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ExtraNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(new ExtraFormModel
            {
                Name = extraData.Name,
                //Transmission = extraData.Transmission,
                Description = extraData.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ExtraFormModel model)
        {
            var extraExists = await this.extraService.ExistsAsync(id);
            if (!extraExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ExtraNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var success = await this.extraService.UpdateAsync(
                id,
                model.Name,
               // model.Transmission,
                model.Description);

            if (!success)
            {
                this.TempData.AddErrorMessage(string.Format(
                    WebAdminConstants.ExtraExistsMsg,
                    model.Name.ToStrongHtml()));

                return this.View(model);
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.ExtraUpdatedMsg,
                model.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var extraData = await this.extraService
                .GetByIdAsync<AdminExtraBasicServiceModel>(id);

            if (extraData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ExtraNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(extraData);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var extraData = await this.extraService
                .GetByIdAsync<AdminExtraBasicServiceModel>(id);

            if (extraData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ExtraNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.ExtraDeletedMsg,
                extraData.Name.ToStrongHtml()));

            await this.extraService.RemoveAsync(id);

            return this.RedirectToAction(nameof(Index));
        }
    }
}
