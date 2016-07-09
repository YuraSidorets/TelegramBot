﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using TelegramBot.Utils;

namespace TelegramBot.DataHelpers
{
    public class Recode
    {
        public static string Url = "http://www.recode.net/";
        /// <summary>
        /// Get Daily news from Recode
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

            var aTags = document?.DocumentNode.SelectNodes("//a[@data-analytics-link='article']");

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
