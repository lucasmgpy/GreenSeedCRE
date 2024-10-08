using Microsoft.AspNetCore.Identity;

namespace GreenSeedCREdev.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }
        public virtual ICollection<PhotoChallenge>? PhotoChallenges { get; set; }
        public virtual ICollection<CommunityPhotoUpload>? CommunityPhotoUploads { get; set; }
        public virtual ICollection<PhotoChallengeParticipation>? PhotoChallengeParticipations { get; set; }
    }
}
