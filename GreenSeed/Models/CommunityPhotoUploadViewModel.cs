using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GreenSeed.ViewModels
{
    public class CommunityPhotoUploadViewModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Foto")]
        public IFormFile Photo { get; set; }
    }
}
