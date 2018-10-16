namespace CarShop.Data.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Dealer
    { 
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.DealerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.DealerDescriptionMaxLength)]
        public string Description { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public ICollection<CarDealer> Cars { get; set; } = new List<CarDealer>();
    }
}
