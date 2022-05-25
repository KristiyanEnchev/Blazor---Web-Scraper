namespace BetterAmazon.Services.ServerServices.ScrapingService.BookScraping
{
    using BetterAmazon.Models.DTO.BooksScraperDTO;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookScrapingService
    {
        //Task<List<ScrapedBookDto>> GetAllBooks();
        Task GetAllBooks();
    }
}
