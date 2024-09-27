using System.ComponentModel.DataAnnotations;

namespace GreenSeedCRE.Models
{
    public class ChallengeOption
    {
        [Key]
        public int OptionId { get; set; }

        // Chave estrangeira
        public int ChallengeId { get; set; }

        [Required]
        public string OptionText { get; set; }

        public bool IsCorrect { get; set; }

        // Navegação
        public virtual PhotoChallenge PhotoChallenge { get; set; }
    }
}