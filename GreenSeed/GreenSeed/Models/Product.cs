using System.ComponentModel.DataAnnotations;

namespace GreenSeedCRE.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        // Chave estrangeira
        public int CategoryId { get; set; }

        // Navegação
        public virtual Category Category { get; set; }
    }
}