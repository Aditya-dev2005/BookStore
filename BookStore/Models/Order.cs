using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string SessionId { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Confirmed";

        public List<OrderItem>? OrderItems { get; set; }
    }
}
