using System.Collections.Generic;
using System.Text;

using HtmlAgilityPack;

using TelegramBot.Utils;

namespace TelegramBot.Services
{
    public class NewsFormer : INewsService
    {
        /// <summary>
        /// Get Daily news from ITC
        /// </summary>
        /// <returns></returns>
        public string ITC()
        {
            return this.GetNews("http://itc.ua/", "//a[@rel = 'bookmark']");
        }

        /// <summary>
        /// Get Daily news from Habr
        /// </summary>
        /// <returns></returns>
        public string Habr()
        {
            return this.GetNews("https://habrahabr.ru/top/", "//a[@class='post__title_link']");
        }

        /// <summary>
        /// Get Daily news from Recode
        /// </summary>
        /// <returns></returns>
        public string Recode()
        {
            return this.GetNews("https://www.recode.net/", "//a[@data-chorus-optimize-field=\"hed\"]");
        }

        /// <summary>
        /// Get Daily news from url
        /// </summary>
        /// <param name="url">web page url</param>
        /// <param name="pattern">XPath string to recognize articles headers</param>
        /// <returns></returns>
        private string GetNews(string url, string pattern)
        {
            StringBuilder builder = new StringBuilder();
            List<string> headers = new List<string>();

            var data = DataCache.GetCachedData(url);

            if (data != null)
            {
                headers = this.ParseHtml(data,pattern);
            }

            if (headers.Count != 0)
            {
                for (var i = 0; i < 5; i++)
                {
                    builder.AppendFormat($"{headers[i]}\n\n");
                }
                //foreach (var header in headers)
                //{
                //    builder.AppendFormat($"{header}\n\n");
                //}
            }
            else
            {
                return "Error";
            }

            return builder + url + "\n\n /help";
        }


        private List<string> ParseHtml(HtmlDocument document, string pattern)
        {
            List<string> contentStrings = new List<string>();

            var aTags = document?.DocumentNode.SelectNodes(pattern);

            if (aTags != null)
            {
                foreach (var tag in aTags)
                {
                    contentStrings.Add(tag.InnerText);
                }
            }
            return contentStrings;
        }
    }
}