using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommereceBookStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = null!;

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // Status values: Pending, Processing, Shipped, Delivered, Cancelled
        public string Status { get; set; } = "Pending";

        [MaxLength(500)]
        public string? ShippingAddress { get; set; }   // <-- THIS fixes CS0117

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}