using System.ComponentModel.DataAnnotations;

namespace Blok1.ViewModels
{
    public class ProductRevenueViewModel
    {
        [DataType(DataType.Text)]
        public string ProductName { get; set; } = null!; 

        [DataType(DataType.Currency)]
        public decimal Revenue { get; set; }
    }
}
