using Microsoft.AspNetCore.Mvc.Formatters;
using static System.Net.Mime.MediaTypeNames;

namespace GreenSeedCRE.Models
{
    public class Challenge
    {
    
            public int ChallengeId { get; set; }
            public DateTime ChallengeDate { get; set; }
            public Image Image { get; set; } // Property to hold the image
            public bool CorrectAnswer { get; set; }
            public ICollection<ChallengeOp> ChallengeOptions { get; set; }
        
    }
}

