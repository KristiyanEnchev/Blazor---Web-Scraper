using BetterAmazon.Models.DTO.ProductDTOs;
using BetterAmazon.Services.ClientServices.ProductService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BetterAmazon.Services.ClientServices.ScrapingService.SoftwareScraping
{
    public class SoftwareScrapingService : ISoftwareScrapingService
    {
        private readonly HttpClient _http;

        private readonly IProductService ProductService;

        public event Action OnChange;

        public SoftwareScrapingService(HttpClient http, IProductService productService)
        {
            _http = http;
            ProductService = productService;
        }

        public async Task ScrapeSoftware()
        {
            ProductService.Products = await _http.GetFromJsonAsync<List<GetAllProductsDto>>("api/SoftwareScraping/software");
            OnChange.Invoke();
        }
    }
}
