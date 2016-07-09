using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using TelegramBot.Utils;

namespace TelegramBot.DataHelpers
{
    public class Habr
    {
        public static string Url = "https://habrahabr.ru/top/";
        /// <summary>
        /// Get Daily news from Habrahabr
        /// </summary>
        /// <returns>Array of strings with articles headers</returns>
        public static string GetNews()
        {
            StringBuilder builder = new StringBuilder();
            List<string> headers = new List<string>();

            var data = DataCache.GetCachedData(Url);

            if (data != null)
            {
                headers = ParseHtml(data);
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

            return builder + Url;
        }


        private static List<string> ParseHtml(HtmlDocument document)
        {
            List<string> contentStrings = new List<string>();

            var aTags = document?.DocumentNode.SelectNodes("//a[@class='post__title_link']");

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