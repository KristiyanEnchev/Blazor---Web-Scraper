namespace BetterAmazon.Services.ClientServices.CategoryService
{
    using BetterAmazon.Models.DTO.CategoryDTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        List<GetCategoryDto> Categories { get; set; }
        Task<List<GetCategoryDto>> LoadCategories();
    }
}
