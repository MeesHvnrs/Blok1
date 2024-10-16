﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Blok1.Data.Models

{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? GifPath { get; set; } 
        [NotMapped]
        public IFormFile? GifFile { get; set; }

        public string? Comment { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public bool ColorChange { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; } = null!;
    }
}