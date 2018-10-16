namespace CarShop.Data.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    //the clients - users could comment every car bought or just for fun
    public class Comment   
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.CommentTitleMinLength)] 
        [MaxLength(DataConstants.CommentTitleMaxLength)] 
        public string Title { get; set; } 

        [Required]
        [MinLength(DataConstants.CommentContentsMinLength)] 
        public string Contents { get; set; } 

        [Required]
        [DataType(DataType.Date)] 
        public DateTime? ReleaseDate { get; set; } 

        public string AuthorId { get; set; } 

        public User Author { get; set; } 

        public int CarId { get; set; } 

        public Car Car { get; set; } 
    }
}
