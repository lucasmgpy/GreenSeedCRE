namespace GreenSeed.Models
{
    public class OrderViewModel
    {
        public decimal TotalAmount { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public IEnumerable<Product> Products { get; set; }

        //public List<Category> Categories { get; set; }
        //public int? SelectedCategoryId { get; set; }
    }
}
