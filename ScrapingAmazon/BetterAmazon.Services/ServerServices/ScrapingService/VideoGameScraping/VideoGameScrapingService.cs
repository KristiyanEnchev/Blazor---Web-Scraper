namespace BetterAmazon.Services.ServerServices.ScrapingService.VideoGameScraping
{
    using AutoMapper;
    using BetterAmazon.Data;
    using BetterAmazon.Data.Models;
    using BetterAmazon.Models.DTO.VideoGamesScraperDTO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Scrapers.VideoGamesScraper;

    public class VideoGameScrapingService : IVideoGameScrapingService
    {
        private readonly BetterAmazonDbContext _context;
        private readonly IMapper _mapper;

        public VideoGameScrapingService(BetterAmazonDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task GetAllGames()
        {
            List<VideoGameDto> books = await GetData();

            List<Game> products = new List<Game>();

            foreach (var item in books)
            {
                if (!_context.Games.Any(x => x.Title == item.Title))
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

                    Game game = this._mapper.Map<Game>(item);
                    game.CategoryId = 3;

                    products.Add(game);
                }
            }

            var dtoDbObject = _mapper.ProjectTo<VideoGameDto>(_context.Games).ToList();

            foreach (var item in books)
            {
                if (dtoDbObject.Contains(item))
                {
                    var productFromDb = _mapper.Map<Game>(item);
                    _context.Remove(productFromDb);
                }
            }

            _context.Games.AddRange(products);

            _context.SaveChanges();

        }
    }
}
