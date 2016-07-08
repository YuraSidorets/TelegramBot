using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TelegramBot.Utils
{
    public static class ITC
    {
        public static string Url = "http://itc.ua/";
        /// <summary>
        /// Get Daily news from ITC
        /// </summary>
        /// <returns>Array of strings with articles headers</returns>
        public static string GetNews()
        {
            StringBuilder builder = new StringBuilder();

            var headers = ParseItcHtml(DataCache.GetCachedData(Url));

            foreach (var header in headers)
            {
                builder.AppendFormat($"{header}\n\n");
            }

            return builder+Url;
        }


        private static List<string> ParseItcHtml(HtmlDocument document)
        {
            List<string> contentStrings = new List<string>();

            var aTags = document?.DocumentNode.SelectNodes("//a");


            if (aTags != null)
            {
                foreach (var tag in aTags)
                {
                    if (tag?.Attributes["rel"]?.Value == "bookmark")
                    {
                        contentStrings.Add(tag.InnerText);
                    }
                }
            }
            return contentStrings;
        }

    }
}