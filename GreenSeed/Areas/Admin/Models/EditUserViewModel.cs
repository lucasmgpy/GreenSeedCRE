using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenSeed.Areas.Admin.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();

        public List<string> AllRoles { get; set; } = new List<string>();
    }
}
