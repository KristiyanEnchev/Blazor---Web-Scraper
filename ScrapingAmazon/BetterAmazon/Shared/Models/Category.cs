namespace BetterAmazon.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static ModelsConstants;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        public string Url { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        public string Icon { get; set; }
    }
}
