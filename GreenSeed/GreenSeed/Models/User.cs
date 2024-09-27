using Microsoft.AspNetCore.Identity;

namespace GreenSeedCRE.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string NIF { get; set; }
        public DateTime SingupDate { get; set; }
        public string? Adress { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
