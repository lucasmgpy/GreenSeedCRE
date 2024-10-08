using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSeedCREdev.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "O preço é obrigatório.")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoryId { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";

        [ValidateNever]
        public Category? Category { get; set; }
        [ValidateNever]
        public ICollection<OrderItem>? OrderItems { get; set; }

    }
}