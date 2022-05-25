namespace BetterAmazon.Server.Controllers
{
    using BetterAmazon.Models.DTO.CategoryDTOs;
    using BetterAmazon.Services.ServerServices.CategoryService;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCategoryDto>>> GetCategories()
        {
            return Ok(await _categoryService.GetCategories());
        }
    }
}
