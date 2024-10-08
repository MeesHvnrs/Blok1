using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blok1.Data.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public bool Afgehandeld { get; set; }


    // koppeltabel
    public ICollection<Orderline> OrderProducts { get; set; } = new List<Orderline>();
    }

}
