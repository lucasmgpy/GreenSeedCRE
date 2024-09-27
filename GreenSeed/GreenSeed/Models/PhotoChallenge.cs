using GreenSeed.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace GreenSeedCRE.Models
{
    public class PhotoChallenge
    {

        [Key]
        public int ChallengeId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [Url]
        public string PhotoUrl { get; set; }

        // Chave estrangeira
        public int AdminId { get; set; }

        // Navegação
        public virtual Admin Admin { get; set; }
        public virtual ICollection<ChallengeOption> ChallengeOptions { get; set; }
        public virtual ICollection<PhotoChallengeParticipation> Participations { get; set; }

    }
}

