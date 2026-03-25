namespace ECommereceBookStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}