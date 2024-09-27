using GreenSeedCRE.Models;
using System.ComponentModel.DataAnnotations;

namespace GreenSeed.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        // Chaves estrangeiras
        public int CartId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        // Navegação
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
