using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GreenSeedCREdev.Models
{
    public class ChallengeOption
    {
        public int ChallengeOptionId { get; set; }
        public int PhotoChallengeId { get; set; }
        public string? OptionText { get; set; }
        public bool IsCorrect { get; set; }

        [ValidateNever]
        public virtual PhotoChallenge? PhotoChallenge { get; set; }
    }
}