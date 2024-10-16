using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSeed.Models
{
    public class ChallengeResponse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ChallengeId { get; set; }

        [ForeignKey("ChallengeId")]
        public Challenge Challenge { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        [Range(1, 4)]
        public int SelectedOption { get; set; } // Valores 1-4 indicando a opção selecionada

        public bool IsCorrect { get; set; }

        public DateTime RespondedAt { get; set; } = DateTime.UtcNow;

        public int PointsAwarded { get; set; }
    }
}
