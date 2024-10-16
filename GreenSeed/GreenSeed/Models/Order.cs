using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GreenSeed.Models
{
    public class Order //Domain Model
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? UserId { get; set; }
        [ValidateNever]
        public ApplicationUser? User { get; set; }
        public decimal TotalAmount { get; set; }
        [ValidateNever]
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
