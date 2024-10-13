using System.ComponentModel.DataAnnotations;

namespace GreenSeedCREdev.Models.ViewModels
{
    public class DeleteUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Nome de utilizador")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
