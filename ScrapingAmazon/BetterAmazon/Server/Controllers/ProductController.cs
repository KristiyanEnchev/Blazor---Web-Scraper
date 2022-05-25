namespace BetterAmazon.Server.Controllers
{
    using BetterAmazon.Models.DTO.ProductDTOs;
    using BetterAmazon.Services.ServerServices.ProductService;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllProductsDto>>> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("Category/{categoryUrl}")]
        public async Task<ActionResult<List<GetAllProductsDto>>> GetProductsByCategory(string categoryUrl)
        {
            return Ok(await _productService.GetProductsByCategory(categoryUrl));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAllProductsDto>> GetProduct(string categoryUrl, int id)
        {
            return Ok(await _productService.GetProduct(categoryUrl, id));
        }

        [HttpGet("bestProducts")]
        public async Task<ActionResult<Dictionary<int, List<GetAllProductsDto>>>> GetBestProductsInfo()
        {
            return Ok(await _productService.GetBestProducts());
        }
    }
}
