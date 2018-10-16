namespace CarShop.Data.EntityModels
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum CarType
    {
        Hatchback = 0,
        Sedan = 1,
        [Display(Name = "Multi-Purpose Vehicle")]
        MPV = 2,
        [Display(Name = "Sports Utility Vehicle")]
        SUV = 3,
        Crossover = 4,  //jeeps
        Coupe = 5,
        Cabrio = 6,
    }
}
