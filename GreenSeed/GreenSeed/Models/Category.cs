namespace GreenSeedCRE.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public Image Image { get; set; } // Property to hold the image
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
