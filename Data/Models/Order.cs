using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blok1.Data.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = "Pending";  // Status (bij tijd over)

        // koppeltabel
        public ICollection<Orderline> OrderProducts { get; set; } = new List<Orderline>();
    }

}
