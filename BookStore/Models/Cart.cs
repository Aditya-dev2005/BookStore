using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public string SessionId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<CartItem>? CartItems { get; set; }
    }
}
