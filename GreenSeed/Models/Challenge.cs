using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSeed.Models
{
    public class Challenge
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; }

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
        public int CorrectOption { get; set; } // Valores 1-4 indicando a opção correta

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsArchived { get; set; } = false;
        public ICollection<ChallengeResponse> ChallengeResponses { get; set; }

        // Método para obter as opções como uma lista
        public List<string> GetOptions()
        {
            return new List<string> { Option1, Option2, Option3, Option4 };
        }
    }
}
