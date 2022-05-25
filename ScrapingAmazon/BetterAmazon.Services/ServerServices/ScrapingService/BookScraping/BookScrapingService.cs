namespace BetterAmazon.Services.ServerServices.ScrapingService.BookScraping
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using BetterAmazon.Data;
    using BetterAmazon.Data.Models;
    using BetterAmazon.Models.DTO.BooksScraperDTO;

    using static Scrapers.BooksScraper;

    public class BookScrapingService : IBookScrapingService
    {
        private readonly BetterAmazonDbContext _context;
        private readonly IMapper _mapper;

        public BookScrapingService(BetterAmazonDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task GetAllBooks()
        {
            List<ScrapedBookDto> books = await GetData();

            List<Book> products = new List<Book>();

            foreach (var item in books)
            {
                if (!_context.Books.Any(x => x.Title + " " + x.Author == item.Title + " " + item.Author))
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

                    var productR = new Book
                    {
                        Title = item.Title,
                        Author = item.Author,
                        ShortDescription = item.ShortDescription,
                        ImageSource = item.ImageSource,
                        ProductSourceUrl = item.ProductSourceUrl,
                        CategoryId = 1,
                        Rating = parsedRating,
                        Price = parsedPrice,
                        InitialPrice = parsedInitialPrice
                    };

                    products.Add(productR);
                }
            }

            //var dtoDbObject = _mapper.ProjectTo<ScrapedBookDto>(_context.Products).ToList();

            //foreach (var item in books)
            //{
            //    if (dtoDbObject.Contains(item))
            //    {
            //        var productFromDb = _context.Products.Where(x => x.Title == item.Title);
            //        _context.Remove(productFromDb);
            //    }
            //}

            _context.Books.RemoveRange(_context.Books);

            _context.Books.AddRange(products);

            _context.SaveChanges();

        }
    }
}
