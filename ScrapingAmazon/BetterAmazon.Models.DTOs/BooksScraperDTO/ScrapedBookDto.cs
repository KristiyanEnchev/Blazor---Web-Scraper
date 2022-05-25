namespace BetterAmazon.Models.DTO.BooksScraperDTO
{
    public class ScrapedBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ShortDescription { get; set; }
        public string Price { get; set; }
        public string InitialPrice { get; set; }
        public string ImageSource { get; set; }
        public string ProductSourceUrl { get; set; }
        public string Rating { get; set; }
    }
}
