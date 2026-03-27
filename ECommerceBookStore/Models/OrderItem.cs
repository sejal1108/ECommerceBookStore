using System.ComponentModel.DataAnnotations.Schema;

namespace ECommereceBookStore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null;
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; } = null;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Subtotal => Price * Quantity;
    }
}