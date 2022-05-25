namespace BetterAmazon.Services.ServerServices.ProductService
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BetterAmazon.Data;
    using BetterAmazon.Models.DTO.CategoryDTOs;
    using BetterAmazon.Models.DTO.ProductDTOs;
    using BetterAmazon.Services.ServerServices.CategoryService;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly BetterAmazonDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ICategoryService categoryService, BetterAmazonDbContext context, IMapper mapper)
        {
            this._categoryService = categoryService;
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<GetAllProductsDto>> GetAllProducts()
        {
            List<GetAllProductsDto> products = new List<GetAllProductsDto>();

            var books = await _context.Books
                .ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                .ToListAsync();

            var softwares = await _context.Softwares
                .ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                .ToListAsync();

            var games = await _context.Games
                .ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                .ToListAsync();

            foreach (var book in books)
            {
                products.Add(book);
            }

            foreach (var software in softwares)
            {
                products.Add(software);

            }

            foreach (var game in games)
            {
                products.Add(game);
            }

            return products;
        }

        public async Task<GetAllProductsDto> GetProduct(string categoryUrl, int id)
        {
            GetAllProductsDto product = new GetAllProductsDto();

            if (categoryUrl.ToLower() == "books")
            {
                product = await _context.Books
               .ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(x => x.Id == id);
            }
            else if (categoryUrl.ToLower() == "video-games")
            {
                product = await _context.Games
                .ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            else if (categoryUrl.ToLower() == "software")
            {
                product = await _context.Softwares
                .ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            }

            return product;
        }

        public async Task<List<GetAllProductsDto>> GetProductsByCategory(string categoryUrl)
        {
            GetCategoryDto category = await _categoryService.GetCategoryByUrl(categoryUrl);

            List<GetAllProductsDto> products = new List<GetAllProductsDto>();

            if (category.Id == 1)
            {
                products = await _context.Books
                    .ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            else if (category.Id == 2)
            {
                products = await _context.Softwares
                   .ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                   .ToListAsync();
            }
            else if (category.Id == 3)
            {
                products = await _context.Games
                   .ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                   .ToListAsync();
            }

            return products;
        }

        public async Task<Dictionary<int, List<GetAllProductsDto>>> GetBestProducts()
        {
            Dictionary<int, List<GetAllProductsDto>> info = new Dictionary<int, List<GetAllProductsDto>>();

            List<int> categories = await _context.Categories.Select(x => x.Id).ToListAsync();

            var books = await _context.Books.
                ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                .Where(x => x.CategoryId == 1)
                .OrderBy(x => x.Rating)
                .Take(3)
                .ToListAsync();

            var software = await _context.Softwares.
                ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                .Where(x => x.CategoryId == 2)
                .OrderBy(x => x.Rating)
                .Take(3)
                .ToListAsync();

            var games = await _context.Games.
                ProjectTo<GetAllProductsDto>(this._mapper.ConfigurationProvider)
                .Where(x => x.CategoryId == 3)
                .OrderBy(x => x.Rating)
                .Take(3)
                .ToListAsync();

            info.Add(1, books);
            info.Add(2, software);
            info.Add(3, games);

            return info;
        }
    }
}
