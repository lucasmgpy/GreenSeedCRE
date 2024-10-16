using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using GreenSeed.Areas.Admin.Controllers;
using GreenSeed.Areas.Admin;

namespace GreenSeed.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        [ValidateNever]
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        [ValidateNever]
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}