namespace BetterAmazon.Services.ServerServices.ScrapingService.SoftwareScraping
{
    using System.Threading.Tasks;

    public interface ISoftwareScrapingService
    {
        Task GetAllSoftware();
    }
}
