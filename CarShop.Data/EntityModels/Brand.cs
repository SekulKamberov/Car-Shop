namespace CarShop.Data.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.BrandNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.BrandDescriptionMaxLength)]
        public string Description { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
