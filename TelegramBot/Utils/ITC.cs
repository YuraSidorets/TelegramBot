using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TelegramBot.Utils
{
    public static class ITC
    {
        /// <summary>
        /// Get Daily news from ITC
        /// </summary>
        /// <returns>Array of strings with articles headers</returns>
        public static string GetNews()
        {
            string url = "http://itc.ua";

            StringBuilder builder = new StringBuilder();

            var headers = ParseItcHtml(DataGrabber.GrabHtml(url));
            foreach (var header in headers)
            {
                builder.AppendFormat($"{header}\n\n");
            }

            return (builder.ToString());
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