namespace BetterAmazon.Services.ClientServices.ProductService
{
    using BetterAmazon.Models.DTO.ProductDTOs;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        event Action OnChange;
        List<GetAllProductsDto> Products { get; set; }
        Task<List<GetAllProductsDto>> LoadProducts(string categoryUrl = null);
        Task<GetAllProductsDto> GetProduct(string categoryUrl, int id);
        Task<Dictionary<int, List<GetAllProductsDto>>> ImportBest();
    }
}
