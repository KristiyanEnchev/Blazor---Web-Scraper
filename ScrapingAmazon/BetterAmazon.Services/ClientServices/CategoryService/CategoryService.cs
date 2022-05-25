namespace BetterAmazon.Services.ClientServices.CategoryService
{
    using BetterAmazon.Models.DTO.CategoryDTOs;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public List<GetCategoryDto> Categories { get; set; } = new List<GetCategoryDto>();

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<GetCategoryDto>> LoadCategories()
        {
            Categories = await _http.GetFromJsonAsync<List<GetCategoryDto>>("api/Category");
            return Categories;
        }
    }
}
