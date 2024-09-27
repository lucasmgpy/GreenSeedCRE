using System.ComponentModel.DataAnnotations;

namespace GreenSeedCRE.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        // Chave estrangeira
        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        [StringLength(50)]
        public string Status { get; set; } // e.g., "Pending", "Shipped", "Delivered"

        // Chave estrangeira
        public int DeliveryCompanyId { get; set; }

        // Navegação
        public virtual User User { get; set; }
        public virtual DeliveryCompany DeliveryCompany { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
