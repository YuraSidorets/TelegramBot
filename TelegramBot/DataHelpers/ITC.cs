using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using TelegramBot.Utils;

namespace TelegramBot.DataHelpers
{
    public class ITC
    {
        public static string Url = "http://itc.ua/";
        /// <summary>
        /// Get Daily news from ITC
        /// </summary>
        /// <returns>Array of strings with articles headers</returns>
        public static string GetNews()
        {
            StringBuilder builder = new StringBuilder();
            List<string> headers = new List<string>();
            var data = DataCache.GetCachedData(Url);

            if (data != null)
            {
                headers  = ParseHtml(data);
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

            return builder+Url;
        }


        private static List<string> ParseHtml(HtmlDocument document)
        {
            List<string> contentStrings = new List<string>();

            var aTags = document?.DocumentNode.SelectNodes("//a[@rel = 'bookmark']");


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