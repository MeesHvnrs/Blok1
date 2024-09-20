namespace Blok1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Comment { get; set; } = null!;
        public double Price { get; set; }
        public bool ColorChange { get; set; }
    }
}
