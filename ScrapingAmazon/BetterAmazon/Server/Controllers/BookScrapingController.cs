namespace BetterAmazon.Server.Controllers
{
    using BetterAmazon.Services.ServerServices.ScrapingService.BookScraping;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class BookScrapingController : ControllerBase
    {
        private readonly IBookScrapingService scraping;

        public BookScrapingController(IBookScrapingService scraping)
        {
            this.scraping = scraping;
        }

        [HttpGet]
        [Route("books")]
        public async Task<ActionResult> ImportBooks()
        {
            await scraping.GetAllBooks();

            return Ok();
        }
    }
}
