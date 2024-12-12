using System.ComponentModel.DataAnnotations;

namespace GreenSeed.Models
{
    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantity { get; set; }
        public decimal Price { get; set; } 
    }
}