namespace BetterAmazon.Client
{
    using BetterAmazon.Services.ClientServices.CategoryService;
    using BetterAmazon.Services.ClientServices.ProductService;
    using BetterAmazon.Services.ClientServices.ScrapingService.BookScraping;
    using BetterAmazon.Services.ClientServices.ScrapingService.SoftwareScraping;
    using BetterAmazon.Services.ClientServices.ScrapingService.VideoGameScraping;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBookScrapingService, BookScrapingService>();
            builder.Services.AddScoped<IVideoGameScrapingService, VideoGameScrapingService>();
            builder.Services.AddScoped<ISoftwareScrapingService, SoftwareScrapingService>();

            await builder.Build().RunAsync();
        }
    }
}
