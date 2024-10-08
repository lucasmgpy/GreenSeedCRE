using GreenSeedCREdev.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GreenSeedCREdev.Models
{
    public class DeliveryCompany
    {
        public int DeliveryCompanyId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        [ValidateNever]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
