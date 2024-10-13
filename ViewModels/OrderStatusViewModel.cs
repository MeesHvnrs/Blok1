using Blok1.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blok1.ViewModels
{
    public class OrderStatusViewModel
    {
        public int OrderId { get; set; }

        public OrderStatus Status { get; set; }

    }
}
