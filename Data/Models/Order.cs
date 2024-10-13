using Blok1.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blok1.Data.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Column(TypeName= "VARCHAR(100)")]
        public OrderStatus Status { get; set; } = OrderStatus.New;

        // koppeltabel
        public ICollection<Orderline> OrderProducts { get; set; } = new List<Orderline>();
    }

}
