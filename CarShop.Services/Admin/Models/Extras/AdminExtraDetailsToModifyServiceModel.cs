namespace CarShop.Services.Admin.Models.Dealers
{
    using CarShop.Data;
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminExtraDetailsToModifyServiceModel : IMapFrom<Extra>
    {
        public string Name { get; set; }

        public Transmission Transmission { get; set; }

        public string Description { get; set; }
    }
}
