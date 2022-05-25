namespace BetterAmazon.Services.ServerServices.ScrapingService.VideoGameScraping
{
    using System.Threading.Tasks;

    public interface IVideoGameScrapingService
    {
        Task GetAllGames();
    }
}
