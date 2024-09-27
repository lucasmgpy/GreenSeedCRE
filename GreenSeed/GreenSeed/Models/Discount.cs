using GreenSeedCRE.Models;
using System.ComponentModel.DataAnnotations;

namespace GreenSeed.Models
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Range(0, 100)]
        public float Percentage { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        // Chave estrangeira
        public int UserId { get; set; }

        // Navegação
        public virtual User User { get; set; }
    }
}

