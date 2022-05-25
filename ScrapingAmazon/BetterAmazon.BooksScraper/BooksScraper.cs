namespace BetterAmazon.Scrapers
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using BetterAmazon.Models.DTO.BooksScraperDTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class BooksScraper
    {
        private const string baseUrl = "https://www.amazon.co.uk/";
        private const string url1 = "https://www.amazon.co.uk/s?i=digital-text&bbn=341689031&rh=n%3A341689031%2Cp_n_date%3A367746031&dc&qid=1638264831&rnid=367744031&ref=sr_pg_1";
        private const string url2 = "https://www.amazon.co.uk/s?i=digital-text&bbn=341689031&rh=n%3A341689031%2Cp_n_date%3A367746031&dc&page=2&qid=1638264146&rnid=367744031&ref=sr_pg_2";
        private const string url3 = "https://www.amazon.co.uk/s?i=digital-text&bbn=341689031&rh=n%3A341689031%2Cp_n_date%3A367746031&dc&page=3&qid=1638264785&rnid=367744031&ref=sr_pg_3";
        private const string url4 = "https://www.amazon.co.uk/s?i=digital-text&bbn=341689031&rh=n%3A341689031%2Cp_n_date%3A367746031&dc&page=4&qid=1638283005&rnid=367744031&ref=sr_pg_4";
        private const string url5 = "https://www.amazon.co.uk/s?i=digital-text&bbn=341689031&rh=n%3A341689031%2Cp_n_date%3A367746031&dc&page=5&qid=1638283009&rnid=367744031&ref=sr_pg_5";
        private const string url6 = "https://www.amazon.co.uk/s?i=digital-text&bbn=341689031&rh=n%3A341689031%2Cp_n_date%3A367746031&dc&page=6&qid=1638283022&rnid=367744031&ref=sr_pg_6";

        public static async Task<List<ScrapedBookDto>> GetData()
        {
            List<ScrapedBookDto> details = new List<ScrapedBookDto>();

            List<string> scrapingUrls = new List<string>(new string[] { url1, url2, url3, /*url4, url5, url6*/ });

            foreach (var url in scrapingUrls)
            {
                var booksList = await ParseHtml(url);

                foreach (var book in booksList)
                {
                    string[] bookTitleAndShortDescription = GetBooksTitleAndShortDescription(book);
                    string[] bookPrices = GetBooksPrice(book);
                    string bookAuthor = GetBookAuthor(book);
                    string bookImg = GetBookImage(book);
                    string bookRating = GetBookRating(book);
                    string bookUrl = GetBookUrl(book, baseUrl);

                    ScrapedBookDto bookModel = new ScrapedBookDto();

                    bookModel.Title = bookTitleAndShortDescription[0];
                    bookModel.Author = bookAuthor;
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

            var mainSelector = ".s-main-slot";
            var element = doc.QuerySelector(mainSelector);
            var booksList = element.QuerySelectorAll("[data-uuid]").ToList();
            if (booksList.Count <= 19)
            {
                booksList = booksList.Take(16).ToList();
            }
            else if (booksList.Count > 19)
            {
                booksList = booksList.Skip(1).Take(16).ToList();
            }
            return booksList;
        }

        private static string GetBookUrl(AngleSharp.Dom.IElement book, string baseUrl)
        {
            //var link = book.QuerySelector(".a-link-normal").GetAttribute("href");
            var completeLink = string.Empty;
            var anchorSelector = ".a-link-normal";
            var anchorElement = book.QuerySelector(anchorSelector);

            if (anchorElement == null)
            {
                return completeLink;
            }

            var link = anchorElement.GetAttribute("href");
            completeLink = baseUrl + link;
            return completeLink;
        }

        private static string[] GetBooksTitleAndShortDescription(AngleSharp.Dom.IElement book)
        {
            var fullTitleInfo = book.QuerySelector(".a-text-normal").TextContent.Split(new char[] { ':', '(', ')' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

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
            var author = string.Empty;
            var titleSelector = ".a-section .a-spacing-none .s-padding-right-small .s-title-instructions-style";
            var titleElement = bookCard.QuerySelector(titleSelector);

            if (titleElement == null)
            {
                return author;
            }

            var anchorSelector = ".a-size-base .a-link-normal .s-underline-text .s-underline-link-text .s-link-style";
            var authorAndPublisherElement = titleElement.QuerySelectorAll(anchorSelector);

            if (authorAndPublisherElement == null)
            {
                return author;
            }

            var book = authorAndPublisherElement[0].TextContent.Split("| ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            author = book[0].Substring(3);
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
            var selector = ".a-section .a-spacing-none .a-spacing-top-small .s-price-instructions-style";
            var pricesElement = book.QuerySelector(selector);

            var bookDiscountePrice = string.Empty;
            var bookMainPrice = string.Empty;

            string[] bookPrices = new string[2];

            if (pricesElement == null)
            {
                bookDiscountePrice = "0";
                bookMainPrice = "0";
                bookPrices[0] = bookDiscountePrice;
                bookPrices[1] = bookMainPrice;
                return bookPrices;
            }

            var pattern = @"(\£\d{1,2}.\d{2})";
            var matches = Regex.Matches(pricesElement.TextContent, pattern);

            if (matches != null && matches.Count != 0)
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
