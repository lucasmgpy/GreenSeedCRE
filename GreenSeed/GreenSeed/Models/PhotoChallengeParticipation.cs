using GreenSeedCRE.Models;
using System.ComponentModel.DataAnnotations;

namespace GreenSeed.Models
{
    public class PhotoChallengeParticipation
    {
        [Key]
        public int ParticipationId { get; set; }

        // Chaves estrangeiras
        public int ChallengeId { get; set; }
        public int UserId { get; set; }

        public int SelectedOptionId { get; set; }

        public DateTime ParticipationDate { get; set; }

        // Navegação
        public virtual PhotoChallenge PhotoChallenge { get; set; }
        public virtual User User { get; set; }
        public virtual ChallengeOption SelectedOption { get; set; }
    }
}
