using GreenSeedCRE.Models;
using System.ComponentModel.DataAnnotations;

namespace GreenSeed.Models
{
    public class DeliveryCompany
    {
        [Key]
        public int DeliveryCompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string ContactInfo { get; set; }

        // Navegação
        public virtual ICollection<Order> Orders { get; set; }
    }
}
