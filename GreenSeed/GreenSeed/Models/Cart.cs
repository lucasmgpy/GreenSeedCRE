using GreenSeedCRE.Models;
using System.ComponentModel.DataAnnotations;

namespace GreenSeed.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        // Chave estrangeira
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navegação
        public virtual User User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
