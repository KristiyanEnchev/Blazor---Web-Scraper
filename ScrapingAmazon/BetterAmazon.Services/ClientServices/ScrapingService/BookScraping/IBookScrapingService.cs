namespace BetterAmazon.Services.ClientServices.ScrapingService.BookScraping
{
    using System;
    using System.Threading.Tasks;

    public interface IBookScrapingService
    {
        event Action OnChange;
        Task ScrapeBooks();
    }
}
