﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenSeed.Models
{
    public class CommunityPhotoUpload
    {
        public int CommunityPhotoUploadId { get; set; }

        public string UserId { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public DateTime UploadDate { get; set; }

        [ValidateNever]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CommunityPhotoComment> Comments { get; set; }
    }
}
