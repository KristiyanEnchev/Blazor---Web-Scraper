using BetterAmazon.Services.ServerServices.ScrapingService.SoftwareScraping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterAmazon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareScrapingController : ControllerBase
    {
        private readonly ISoftwareScrapingService scraping;

        public SoftwareScrapingController(ISoftwareScrapingService scraping)
        {
            this.scraping = scraping;
        }

        [HttpGet]
        [Route("software")]
        public async Task<ActionResult> ImportBooks()
        {
            await scraping.GetAllSoftware();

            return Ok();
        }
    }
}
