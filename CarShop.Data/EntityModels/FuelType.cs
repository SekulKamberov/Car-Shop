namespace CarShop.Data.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public enum FuelType
    {
        Petrol = 0,
        Diesel = 1,
        LPG = 2,
        [Display(Name = "Petrol - LPG Bulgarian modification")]
        PetrolLPG = 3,
        Ethanol = 4,
        Electric = 5,
        [Display(Name = "Electric and Petrol")]
        ElectricPetrol = 6,
    }
}
