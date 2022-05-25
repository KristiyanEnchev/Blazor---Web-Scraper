namespace BetterAmazon.Services.ServerServices.ScrapingService.SoftwareScraping
{
    using AutoMapper;
    using BetterAmazon.Data;
    using BetterAmazon.Data.Models;
    using BetterAmazon.Models.DTO.SoftwareScraperDTO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Scrapers.SoftwareScraper;

    public class SoftwareScrapingService : ISoftwareScrapingService
    {
        private readonly BetterAmazonDbContext _context;
        private readonly IMapper _mapper;

        public SoftwareScrapingService(BetterAmazonDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task GetAllSoftware()
        {
            List<SoftwareDto> books = await GetData();

            List<Software> products = new List<Software>();

            foreach (var item in books)
            {
                if (!_context.Softwares.Any(x => x.Title == item.Title))
                {
                    decimal parsedPrice;
                    decimal parsedInitialPrice;
                    decimal parsedRating;
                    var price = decimal.TryParse(item.Price.ToString(), out parsedPrice);
                    var initialPrice = decimal.TryParse(item.InitialPrice.ToString(), out parsedInitialPrice);
                    var rating = decimal.TryParse(item.Rating.ToString(), out parsedRating);

                    if (!price)
                    {
                        parsedPrice = 0;
                    }
                    if (!initialPrice)
                    {
                        parsedInitialPrice = 0;
                    }
                    if (!rating)
                    {
                        parsedRating = 0;
                    }

                    var temp = parsedPrice;
                    if (parsedPrice > parsedInitialPrice)
                    {
                        parsedPrice = parsedInitialPrice;
                        parsedInitialPrice = temp;
                    }

                    Software software = this._mapper.Map<Software>(item);
                    software.CategoryId = 2;

                    products.Add(software);
                }
            }

            var dtoDbObject = _mapper.ProjectTo<SoftwareDto>(_context.Softwares).ToList();

            foreach (var item in books)
            {
                if (dtoDbObject.Contains(item))
                {
                    var productFromDb = _mapper.Map<Software>(item);
                    _context.Remove(productFromDb);
                }
            }

            _context.Softwares.AddRange(products);

            _context.SaveChanges();

        }
    }
}
