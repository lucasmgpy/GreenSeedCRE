using GreenSeed.Models;
using Microsoft.AspNetCore.Identity;

namespace GreenSeed.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }
    }
}
