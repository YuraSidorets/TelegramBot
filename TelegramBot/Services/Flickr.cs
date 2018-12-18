using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HtmlAgilityPack;

using TelegramBot.Utils;

namespace TelegramBot.Services
{
    public class Flickr : IPhotoService
    {
        /// <summary>
        /// Get Daily news from Recode
        /// </summary>
        /// <returns></returns>
        public string GetPhoto()
        {
            return GetImgLink("https://www.flickr.com/explore/", "//div[@class=\"view photo-list-photo-view requiredToShowOnServer awake\"]"); 
        }

        /// <summary>
        /// Get Daily news from url
        /// </summary>
        /// <param name="url">web page url</param>
        /// <param name="pattern">XPath string </param>
        /// <returns></returns>
        private string GetImgLink(string url, string pattern)
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


        private List<string> ParseHtml(HtmlDocument document, string pattern)
        {
            var contentStrings = new List<string>();

            var src = document.DocumentNode.SelectNodes(pattern);

            if (src == null) return contentStrings;

            foreach (var div in src)
            {
                var link = div.Attributes;
                contentStrings.Add(link[1].Value.Substring(link[1].Value.IndexOf("background-image: url(")).Replace("background-image: url(//", "").Replace(")", ""));
            }

            // background-image: url(//c1.staticflickr.com/5/4864/44537897510_b5d4a27400.jpg)

            //contentStrings.AddRange(from link in links.Split("\n\t".ToCharArray())
            //                        where link.Contains("img.src=")
            //                        let start = link.IndexOf("/c", StringComparison.CurrentCulture)
            //                        select link.Substring(start).Trim("';/".ToCharArray()));
            return contentStrings;
        }
    }
}