namespace GreenSeed.Models
{
    public class ChallengeViewModel
    {
        public Challenge Challenge { get; set; }
        public bool HasResponded { get; set; }
        public ChallengeResponse UserResponse { get; set; }
    }
}
