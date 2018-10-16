namespace CarShop.Services.Admin.Models.Dealers
{
    using CarShop.Common.Mapping;
    using CarShop.Data.EntityModels;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AdminBrandsBasicServiceModel : IMapFrom<Dealer>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
