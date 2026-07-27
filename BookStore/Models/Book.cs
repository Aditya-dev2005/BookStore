using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length of {0} should be between {2} and {1}")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-:',.!?]+$", ErrorMessage = "Title cannot contain special symbols like @ # $ %")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length of {0} should be between {2} and {1}")]
        [RegularExpression(@"^[a-zA-Z\s.]+$", ErrorMessage = "Author name should contain only letters")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, 100000, ErrorMessage = "Price must be between {1} and {2}")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed {1} characters")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        [Range(0, 10000, ErrorMessage = "Stock must be between {1} and {2}")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }
    }
}