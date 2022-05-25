namespace BetterAmazon.Models.DTO.ProductDTOs
{
    public class GetAllProductsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ShortDescription { get; set; }
        public string ImageSource { get; set; } = "https://via.placeholder.com/300x300";
        public string ProductSourceUrl { get; set; }
        public decimal Price { get; set; }
        public decimal InitialPrice { get; set; }
        public decimal Rating { get; set; }
        public int CategoryId { get; set; }
    }
}
