namespace BetterAmazon.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static ModelsConstants;

    public class Book
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(AuthorMaxLength)]
        public string Author { get; set; }

        public string ShortDescription { get; set; }

        [MaxLength(UrlMaxLength)]
        public string ImageSource { get; set; } = "https://via.placeholder.com/300";

        [MaxLength(UrlMaxLength)]
        public string ProductSourceUrl { get; set; }

        [Range(typeof(decimal), decimalTypeMin, decimalTypeMax)]
        public decimal? Rating { get; set; }

        [Range(typeof(decimal), decimalTypeMin, decimalTypeMax)]
        public decimal? Price { get; set; }

        [Range(typeof(decimal), decimalTypeMin, decimalTypeMax)]
        public decimal? InitialPrice { get; set; }


        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; }
    }
}
