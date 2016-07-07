using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TelegramBot.Utils
{
    public static class DataGrabber
    {
        /// <summary>
        /// Html Document grabber
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HtmlDocument GrabHtml(string url)
        {
            var htmlDoc = new HtmlDocument();
            using (var wc = new GZipWebClient())
            {
                wc.Encoding = Encoding.UTF8;
                htmlDoc.LoadHtml(wc.DownloadStringTaskAsync(url).Result);
            }
            return htmlDoc;
        }
    }

    /// <summary>
    /// Web Client for Gzip encoding
    /// </summary>
    public class GZipWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri adress)
        {
            HttpWebRequest request = (HttpWebRequest) base.GetWebRequest(adress);
            request.AutomaticDecompression = DecompressionMethods.GZip |
            DecompressionMethods.Deflate;
            return request;
        }
    }

  


}