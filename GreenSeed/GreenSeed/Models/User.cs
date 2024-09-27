using GreenSeed.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GreenSeedCRE.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } // e.g., "Admin", "Customer"

        public DateTime RegisterDate { get; set; }

        // Navegação
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<CommunityPhotoUpload> CommunityPhotoUploads { get; set; }
        public virtual ICollection<PhotoChallengeParticipation> PhotoChallengeParticipations { get; set; }
        public virtual Discount Discount { get; set; }
    }
    public class Admin : User
    {
        public int AdminLevel { get; set; }

        // Navegação
        public virtual ICollection<PhotoChallenge> PhotoChallenges { get; set; }
    }
}
