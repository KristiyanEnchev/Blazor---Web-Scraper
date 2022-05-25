namespace BetterAmazon.Scrapers
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using BetterAmazon.Models.DTO.SoftwareScraperDTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class SoftwareScraper
    {
        private const string baseUrl = "https://www.amazon.co.uk/";
        private const string url1 = "https://www.amazon.co.uk/s?i=software&bbn=300703&s=review-rank&page=1&qid=1640353615&ref=sr_pg_1";
        private const string url2 = "https://www.amazon.co.uk/s?i=software&bbn=300703&s=review-rank&page=2&qid=1640353615&ref=sr_pg_2";
        private const string url3 = "https://www.amazon.co.uk/s?i=software&bbn=300703&s=review-rank&page=3&qid=1640353615&ref=sr_pg_3";
        private const string url4 = "https://www.amazon.co.uk/s?i=software&bbn=300703&s=review-rank&page=4&qid=1640353615&ref=sr_pg_4";
        private const string url5 = "https://www.amazon.co.uk/s?i=software&bbn=300703&s=review-rank&page=5&qid=1640353615&ref=sr_pg_5";
        private const string url6 = "https://www.amazon.co.uk/s?i=software&bbn=300703&s=review-rank&page=6&qid=1640353615&ref=sr_pg_6";

        public static async Task<List<SoftwareDto>> GetData()
        {
            List<SoftwareDto> details = new List<SoftwareDto>();

            List<string> scrapingUrls = new List<string>(new string[] { url1, url2, url3, url4, url5/*, url6*/ });

            foreach (var url in scrapingUrls)
            {
                var booksList = await ParseHtml(url);

                foreach (var book in booksList)
                {
                    string[] bookTitleAndShortDescription = GetBooksTitleAndShortDescription(book);
                    string[] bookPrices = GetBooksPrice(book);
                    if (bookPrices[0] == "" && bookPrices[1] == "")
                    {
                        continue;
                    }
                    string bookImg = GetBookImage(book);
                    string bookRating = GetBookRating(book);
                    string bookUrl = GetBookUrl(book, baseUrl);

                    SoftwareDto bookModel = new SoftwareDto();

                    bookModel.Title = bookTitleAndShortDescription[0];
                    bookModel.Author = "";
                    bookModel.ShortDescription = bookTitleAndShortDescription[1];
                    bookModel.ImageSource = bookImg;
                    bookModel.ProductSourceUrl = bookUrl;
                    bookModel.Rating = bookRating;
                    bookModel.Price = bookPrices[0];
                    bookModel.InitialPrice = bookPrices[1];

                    details.Add(bookModel);
                }
            }

            return details;
        }

        public static async Task<List<AngleSharp.Dom.IElement>> ParseHtml(string url)
        {
            IHtmlDocument doc = default(IHtmlDocument);
            using (var client = new HttpClient())
            using (var stream = await client.GetStreamAsync(new Uri(url)))
            {
                var parser = new HtmlParser();

                doc = await parser.ParseDocumentAsync(stream);
            }
            var booksList = doc.QuerySelectorAll(".s-asin").Take(16).ToList();
            return booksList;
        }

        private static string GetBookUrl(AngleSharp.Dom.IElement book, string baseUrl)
        {
            var link = book.QuerySelector(".a-link-normal").GetAttribute("href");
            var completeLink = baseUrl + link;
            return completeLink;
        }

        private static string[] GetBooksTitleAndShortDescription(AngleSharp.Dom.IElement book)
        {
            var fullTitleInfo = book.QuerySelector(".a-text-normal").TextContent.Split(new char[] { ':', '(', ')', '|' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var bookTitle = string.Empty;
            var bookShortDescription = string.Empty;
            string[] bookInfo = new string[2];

            if (fullTitleInfo == null)
            {
                return bookInfo;
            }

            if (fullTitleInfo.Length <= 1)
            {
                bookTitle = fullTitleInfo[0];
                bookShortDescription = fullTitleInfo[0];
            }
            else
            {
                bookTitle = fullTitleInfo[0];
                foreach (var item in fullTitleInfo)
                {
                    bookShortDescription += item + " ";
                }
            }

            bookInfo[0] = bookTitle;
            bookInfo[1] = bookShortDescription.Trim();

            return bookInfo;
        }

        private static string GetBookAuthor(AngleSharp.Dom.IElement bookCard)
        {
            var book = bookCard.QuerySelector(".a-size-base").TextContent.Split("| ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            var author = book[0].Substring(3);
            return author;
        }

        private static string GetBookImage(AngleSharp.Dom.IElement book)
        {
            var imageURL = book.QuerySelector(".s-image");

            if (imageURL == null)
            {
                return "";
            }
            return imageURL.GetAttribute("src");
        }

        private static string[] GetBooksPrice(AngleSharp.Dom.IElement book)
        {
            var pricesElement = book.QuerySelector(".sg-col-inner");
            var bookDiscountePrice = string.Empty;
            var bookMainPrice = string.Empty;

            string[] bookPrices = new string[2];

            var pattern = @"(\£\d{1,2}.\d{2})";
            var matches = Regex.Matches(pricesElement.TextContent, pattern);

            if (matches != null && matches.Count > 0)
            {
                bookDiscountePrice = matches[0].Value.Replace("£", "");
                bookMainPrice = matches[matches.Count - 1].Value.Replace("£", "");
            }

            bookPrices[0] = bookDiscountePrice;
            bookPrices[1] = bookMainPrice;

            return bookPrices;
        }

        private static string GetBookRating(AngleSharp.Dom.IElement book)
        {
            var rating = book.QuerySelector(".a-icon-alt");

            if (rating == null)
            {
                return "";
            }
            var ratingSplited = rating.TextContent.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            return ratingSplited[0];
        }
    }
}
