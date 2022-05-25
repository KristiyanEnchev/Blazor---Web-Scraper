namespace BetterAmazon.Services.ServerServices.CategoryService
{
    using BetterAmazon.Models.DTO.CategoryDTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<List<GetCategoryDto>> GetCategories();
        Task<GetCategoryDto> GetCategoryByUrl(string categoryUrl);
    }
}
