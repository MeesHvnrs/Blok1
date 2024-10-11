using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blok1.Data.Models
{
    public class Orderline
    {
            [Key]
            public int Id { get; set; }

            public int OrderId { get; set; }
            public Order? Order { get; set; } = null!;

            public int ProductId { get; set; }
            public Product? Product { get; set; } = null!;

            [Required]
            public string CustomName { get; set; } = null!;

            public string? CustomColor { get; set; }
    }
}
