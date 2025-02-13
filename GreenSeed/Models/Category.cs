﻿using GreenSeed.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GreenSeed.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [ValidateNever]
        public ICollection<Product>? Products { get; set; }
    }
}