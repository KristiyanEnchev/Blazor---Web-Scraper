namespace BetterAmazon.Services.ClientServices.ScrapingService.SoftwareScraping
{
    using System;
    using System.Threading.Tasks;

    public interface ISoftwareScrapingService
    {
        event Action OnChange;
        Task ScrapeSoftware();
    }
}
