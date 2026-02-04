namespace JewelryShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = "";

       
        public decimal Rating { get; set; }
    
        public int Quantity { get; set; }
    }
}
