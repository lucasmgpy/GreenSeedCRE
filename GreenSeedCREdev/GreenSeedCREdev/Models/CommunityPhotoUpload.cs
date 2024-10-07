using GreenSeedCREdev.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GreenSeedCREdev.Models
{
    public class CommunityPhotoUpload
    {
        public int CommunityPhotoUploadId { get; set; }
        public int UserId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }

        [ValidateNever]
        public virtual ApplicationUser User { get; set; }
    }
}
