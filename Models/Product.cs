using System.ComponentModel.DataAnnotations; 
namespace Blok1.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!; 

        public string? Comment { get; set; }
        [Required]
        public double Price { get; set; }

        public bool ColorChange { get; set; }
    }
}


