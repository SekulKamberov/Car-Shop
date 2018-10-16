namespace CarShop.Services.Admin.Models.Cars
{
    using Common.Mapping;
    using Data.EntityModels;

    public class AdminCarBasicServiceModel : IMapFrom<Car>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
