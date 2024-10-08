using System.ComponentModel.DataAnnotations;

namespace Blok1.ViewModels
{
    public class ProductRevenueViewModel
    {
        [DataType(DataType.Text), StringLength(25)]
        public string ProductName { get; set; } = null!;

        [DataType(DataType.Currency)]
        public decimal Revenue { get; set; }

    }
}
