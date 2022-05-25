namespace BetterAmazon.Services.ClientServices.ScrapingService.VideoGameScraping
{
    using BetterAmazon.Models.DTO.ProductDTOs;
    using BetterAmazon.Services.ClientServices.ProductService;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class VideoGameScrapingService : IVideoGameScrapingService
    {
        private readonly HttpClient _http;

        private readonly IProductService ProductService;

        public event Action OnChange;

        public VideoGameScrapingService(HttpClient http, IProductService productService)
        {
            _http = http;
            ProductService = productService;
        }

        public async Task ScrapeGames()
        {
            ProductService.Products = await _http.GetFromJsonAsync<List<GetAllProductsDto>>("api/VideoGameScraping/videoGames");
            OnChange.Invoke();
        }
    }
}
