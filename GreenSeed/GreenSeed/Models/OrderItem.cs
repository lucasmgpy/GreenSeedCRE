using System.ComponentModel.DataAnnotations;

namespace GreenSeedCRE.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        // Chaves estrangeiras
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        // Navegação
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
