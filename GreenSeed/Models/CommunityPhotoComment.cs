using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GreenSeed.Models
{
    public class CommunityPhotoComment
    {
        public int CommunityPhotoCommentId { get; set; }

        public int CommunityPhotoUploadId { get; set; }

        public string UserId { get; set; } // Alterado de int para string

        [Required]
        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; }

        [ValidateNever]
        public virtual CommunityPhotoUpload CommunityPhotoUpload { get; set; }

        [ValidateNever]
        public virtual ApplicationUser User { get; set; }
    }
}
