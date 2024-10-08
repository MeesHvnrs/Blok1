using System.ComponentModel.DataAnnotations;

namespace Blok1.Data.Models
{
    public class Orderline
    {
            [Key]
            public int OrderProductId { get; set; }

            [Required]
            public int OrderId { get; set; }
            public Order Order { get; set; } = null!;

            [Required]
            public int ProductId { get; set; }
            public Product Product { get; set; } = null!;

            [Required]
            public int Quantity { get; set; }

            [Required]
            public string CustomName { get; set; } = null!;

            public string? CustomColor { get; set; }
    }
}
