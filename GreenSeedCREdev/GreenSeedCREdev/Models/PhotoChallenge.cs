using GreenSeedCREdev.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace GreenSeedCREdev.Models
{
    public class PhotoChallenge
    {
        public int PhotoChallengeId { get; set; }
        public DateTime Date { get; set; }
        public string? PhotoUrl { get; set; }
        public int UserId { get; set; }

        [ValidateNever]
        public virtual ApplicationUser? User { get; set; }
        [ValidateNever]
        public virtual ICollection<ChallengeOption>? ChallengeOptions { get; set; }
        [ValidateNever]
        public virtual ICollection<PhotoChallengeParticipation>? Participations { get; set; }

    }
}

