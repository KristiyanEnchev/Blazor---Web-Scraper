namespace BetterAmazon.Server.Controllers
{
    using BetterAmazon.Models.DTO.ProductDTOs;
    using BetterAmazon.Services.ServerServices.ProductService;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareController : ControllerBase
    {
        private readonly IProductService _productService;

        public SoftwareController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAllProductsDto>> GetProduct(int id)
        {
            string categoryUrl = "Software";
            return Ok(await _productService.GetProduct(categoryUrl, id));
        }
    }
}
