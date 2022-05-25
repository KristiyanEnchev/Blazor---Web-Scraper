namespace BetterAmazon.Services.ServerServices.CategoryService
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BetterAmazon.Data;
    using BetterAmazon.Models.DTO.CategoryDTOs;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly BetterAmazonDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(BetterAmazonDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetCategoryDto>> GetCategories()
        {
            return await this._context.Categories
                .ProjectTo<GetCategoryDto>(this._mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GetCategoryDto> GetCategoryByUrl(string categoryUrl)
        {
            return await this._context.Categories
                .ProjectTo<GetCategoryDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Url.ToLower().Equals(categoryUrl.ToLower()));
        }
    }
}
