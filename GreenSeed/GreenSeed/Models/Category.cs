using System.ComponentModel.DataAnnotations;

namespace GreenSeedCRE.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        // Navegação
        public virtual ICollection<Product> Products { get; set; }
    }
}
