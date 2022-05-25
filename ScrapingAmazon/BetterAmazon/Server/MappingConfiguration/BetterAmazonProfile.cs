namespace BetterAmazon.Server.MappingConfiguration
{
    using AutoMapper;
    using BetterAmazon.Data.Models;
    using BetterAmazon.Models.DTO.BooksScraperDTO;
    using BetterAmazon.Models.DTO.CategoryDTOs;
    using BetterAmazon.Models.DTO.ProductDTOs;
    using BetterAmazon.Models.DTO.SoftwareScraperDTO;
    using BetterAmazon.Models.DTO.VideoGamesScraperDTO;

    public class BetterAmazonProfile : Profile
    {
        public BetterAmazonProfile()
        {
            this.CreateMap<Category, GetCategoryDto>();

            this.CreateMap<Book, GetAllProductsDto>();
            this.CreateMap<Game, GetAllProductsDto>();
            this.CreateMap<Software, GetAllProductsDto>();
            this.CreateMap<Book, ImportBestProductsDto>();
            this.CreateMap<Software, ImportBestProductsDto>();
            this.CreateMap<Game, ImportBestProductsDto>();
            this.CreateMap<GetAllProductsDto, ImportBestProductsDto>();

            this.CreateMap<Book, ScrapedBookDto>();
            this.CreateMap<ScrapedBookDto, Book>();

            this.CreateMap<Game, VideoGameDto>();
            this.CreateMap<VideoGameDto, Game>();

            this.CreateMap<Software, SoftwareDto>();
            this.CreateMap<SoftwareDto, Software>();
        }
    }
}
