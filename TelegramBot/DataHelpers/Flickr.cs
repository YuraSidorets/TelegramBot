using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using TelegramBot.Utils;

namespace TelegramBot.DataHelpers
{
    public class Flickr
    {
        /// <summary>
        /// Get Daily news from Recode
        /// </summary>
        /// <returns></returns>
        public static string GetPhoto()
        {
            return GetImgLink("https://www.flickr.com/explore/", "//script[contains(text(), 'img.src')]");
        }

        /// <summary>
        /// Get Daily news from url
        /// </summary>
        /// <param name="url">web page url</param>
        /// <param name="pattern">XPath string </param>
        /// <returns></returns>
        private static string GetImgLink(string url, string pattern)
        {
            StringBuilder builder = new StringBuilder();
            List<string> photos = new List<string>();

            var data = DataCache.GetCachedData(url);

            if (data != null)
            {
                photos = ParseHtml(data, pattern);
            }

            if (photos.Count != 0)
            {
                //foreach (var header in headers)
                //{
                //    builder.AppendFormat($"{header}\n\n");
                //}
                builder.Append(photos[new Random().Next(0, photos.Count)]+ "\n\n");
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

            var Src = document?.DocumentNode.SelectNodes(pattern);

            if (Src != null)
            {
               var links = Src.First().InnerText;
                foreach (var link in links.Split("\n\t".ToCharArray()))
                {
                    if (link.Contains("img.src="))
                    {
                        var start = link.IndexOf("/c", StringComparison.CurrentCulture);
                        contentStrings.Add(link.Substring(start).Trim("';/".ToCharArray()));
                    }
                }
            }
            return contentStrings;
        }
    }
}