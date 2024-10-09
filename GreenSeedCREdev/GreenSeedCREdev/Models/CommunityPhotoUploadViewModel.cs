using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GreenSeedCREdev.ViewModels
{
    public class CommunityPhotoUploadViewModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
