namespace GreenSeedCRE.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public Image Image { get; set; } // Property to hold the image
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}