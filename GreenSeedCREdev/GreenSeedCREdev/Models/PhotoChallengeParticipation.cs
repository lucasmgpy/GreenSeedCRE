using GreenSeedCREdev.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GreenSeedCREdev.Models
{
    public class PhotoChallengeParticipation
    {
        [Key]
        public int PhotoChallengeParticipationId { get; set; }

        // Chaves estrangeiras
        public int ChallengeId { get; set; }
        public int UserId { get; set; }
        public int SelectedOptionId { get; set; }
        public DateTime ParticipationDate { get; set; }

        [ValidateNever]
        public virtual PhotoChallenge? PhotoChallenge { get; set; }
        [ValidateNever]
        public virtual ApplicationUser? User { get; set; }
        [ValidateNever]
        public virtual ChallengeOption? SelectedOption { get; set; }
    }
}
