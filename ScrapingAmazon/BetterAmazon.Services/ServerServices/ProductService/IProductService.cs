namespace BetterAmazon.Services.ServerServices.ProductService
{
    using BetterAmazon.Models.DTO.ProductDTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<List<GetAllProductsDto>> GetAllProducts();
        Task<List<GetAllProductsDto>> GetProductsByCategory(string categoryUrl);
        Task<GetAllProductsDto> GetProduct(string categoryUrl, int id);
        Task<Dictionary<int, List<GetAllProductsDto>>> GetBestProducts();
    }
}
