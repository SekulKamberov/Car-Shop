namespace CarShop.Data.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Extra
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.ExtraNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.ExtraDescriptionMaxLength)]
        public string Description { get; set; }

        public ICollection<CarData> CarInfos { get; set; } = new List<CarData>();
    }
}
