using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GreenSeed.ViewModels
{
    public class EditChallengeViewModel
    {
        [Required]
        public int Id { get; set; } // Necessário para identificar o desafio a ser editado

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

        [Display(Name = "Nova Imagem do Desafio (Opcional)")]
        public IFormFile ImageFile { get; set; } // Opcional para permitir a atualização da imagem

        public string ExistingImagePath { get; set; } // Caminho da imagem existente para exibição
    }
}
