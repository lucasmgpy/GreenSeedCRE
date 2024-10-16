using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GreenSeed.ViewModels
{
    public class CreateChallengeViewModel
    {
        [Required]
        [StringLength(100)]
        public string Option1 { get; set; }

        [Required]
        [StringLength(100)]
        public string Option2 { get; set; }

        [Required]
        [StringLength(100)]
        public string Option3 { get; set; }

        [Required]
        [StringLength(100)]
        public string Option4 { get; set; }

        [Required]
        [Range(1, 4)]
        public int CorrectOption { get; set; }

        [Required]
        [Display(Name = "Imagem do Desafio")]
        public IFormFile ImageFile { get; set; }
    }
}
