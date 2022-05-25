namespace BetterAmazon.Services.ClientServices.ScrapingService.VideoGameScraping
{
    using System;
    using System.Threading.Tasks;

    public interface IVideoGameScrapingService
    {
        event Action OnChange;
        Task ScrapeGames();
    }
}
