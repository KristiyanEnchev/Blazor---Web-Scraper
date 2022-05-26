namespace BetterAmazon.Services.ClientServices.ProductService
{
    using BetterAmazon.Models.DTO.ProductDTOs;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public List<GetAllProductsDto> Products { get; set; } = new List<GetAllProductsDto>();

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<GetAllProductsDto>> LoadProducts(string categoryUrl = null)
        {
            if (categoryUrl == null)
            {
                Products = await _http.GetFromJsonAsync<List<GetAllProductsDto>>("api/Product");
            }
            else
            {
                Products = await _http.GetFromJsonAsync<List<GetAllProductsDto>>($"api/Product/Category/{categoryUrl}");
            }

            OnChange.Invoke();
            return Products;
        }

        public async Task<GetAllProductsDto> GetProduct(string categoryUrl, int id)
        {
            string controllerName = string.Empty;
            if (categoryUrl.ToLower() == "books")
            {
                controllerName = "Books";
            }
            else if (categoryUrl.ToLower() == "video-games")
            {
                controllerName = "VideoGames";
            }
            else if (categoryUrl.ToLower() == "software")
            {
                controllerName = "Software";
            }

            var result = await _http.GetFromJsonAsync<GetAllProductsDto>($"api/{controllerName}/{id}");

            return result;
        }

        public async Task<Dictionary<int, List<GetAllProductsDto>>> ImportBest()
        {
            var result = await _http.GetFromJsonAsync<Dictionary<int, List<GetAllProductsDto>>>($"api/Product/bestProducts");

            return result;
        }
    }
}
