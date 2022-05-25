namespace BetterAmazon.Services.ClientServices.ScrapingService.BookScraping
{
    using BetterAmazon.Models.DTO.ProductDTOs;
    using BetterAmazon.Services.ClientServices.ProductService;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class BookScrapingService : IBookScrapingService
    {
        private readonly HttpClient _http;

        private readonly IProductService ProductService;

        public event Action OnChange;

        public BookScrapingService(HttpClient http, IProductService productService)
        {
            _http = http;
            ProductService = productService;
        }

        public async Task ScrapeBooks()
        {
            ProductService.Products = await _http.GetFromJsonAsync<List<GetAllProductsDto>>("api/BookScraping/books");
            OnChange.Invoke();
        }
    }
}
