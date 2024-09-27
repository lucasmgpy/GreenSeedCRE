using GreenSeedCRE.Models;
using System.ComponentModel.DataAnnotations;

namespace GreenSeed.Models
{
    public class CommunityPhotoUpload
    {
        [Key]
        public int UploadId { get; set; }

        // Chave estrangeira
        public int UserId { get; set; }

        [Required]
        [Url]
        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public DateTime UploadDate { get; set; }

        // Navegação
        public virtual User User { get; set; }
    }
}
