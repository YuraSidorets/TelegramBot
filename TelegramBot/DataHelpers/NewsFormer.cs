using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using TelegramBot.Utils;

namespace TelegramBot.DataHelpers
{
    public class NewsFormer
    {
        /// <summary>
        /// Get Daily news from ITC
        /// </summary>
        /// <returns></returns>
        public static string ITC()
        {
            return GetNews("http://itc.ua/", "//a[@rel = 'bookmark']");
        }

        /// <summary>
        /// Get Daily news from Habr
        /// </summary>
        /// <returns></returns>
        public static string Habr()
        {
            return GetNews("https://habrahabr.ru/top/", "//a[@class='post__title_link']");
        }

        /// <summary>
        /// Get Daily news from Recode
        /// </summary>
        /// <returns></returns>
        public static string Recode()
        {
            return GetNews("http://www.recode.net/", "//a[@data-analytics-link='article']");
        }

        /// <summary>
        /// Get Daily news from url
        /// </summary>
        /// <param name="url">web page url</param>
        /// <param name="pattern">XPath string to recognize articles headers</param>
        /// <returns></returns>
        private static string GetNews(string url, string pattern)
        {
            StringBuilder builder = new StringBuilder();
            List<string> headers = new List<string>();

            var data = DataCache.GetCachedData(url);

            if (data != null)
            {
                headers = ParseHtml(data,pattern);
            }

            if (headers.Count != 0)
            {
                foreach (var header in headers)
                {
                    builder.AppendFormat($"{header}\n\n");
                }
            }
            else
            {
                return "Error";
            }

            return builder + url + "\n\n /help";
        }


        private static List<string> ParseHtml(HtmlDocument document, string pattern)
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