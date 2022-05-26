namespace BetterAmazon.Server.Controllers
{
    using BetterAmazon.Services.ServerServices.ScrapingService.VideoGameScraping;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameScrapingController : ControllerBase
    {
        private readonly IVideoGameScrapingService scraping;

        public VideoGameScrapingController(IVideoGameScrapingService scraping)
        {
            this.scraping = scraping;
        }

        [HttpGet]
        [Route("videoGames")]
        public async Task<ActionResult> ImportBooks()
        {
            await scraping.GetAllGames();

            return Ok();
        }
    }
}
