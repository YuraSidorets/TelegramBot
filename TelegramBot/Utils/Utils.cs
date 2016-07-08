using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
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
                htmlDoc.LoadHtml(wc.DownloadString(url));
            }
            
            //Cache data when grab it 
            DataCache.Cache(url,htmlDoc);

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
            if (request != null)
            {
                request.AutomaticDecompression = DecompressionMethods.GZip |
                                                 DecompressionMethods.Deflate;
                return request;
            }
            return GetWebRequest(adress);
        }
    }

    /// <summary>
    /// Chaching data
    /// </summary>
    public static class DataCache
    {
        private static Cache _cache = HttpRuntime.Cache;
        

        public static void Cache(string url, HtmlDocument document)
        {
            _cache.Add(url,document, null, System.Web.Caching.Cache.NoAbsoluteExpiration,TimeSpan.FromHours(1.0),CacheItemPriority.Default, OnRemoveCallback);
        }

        private static void OnRemoveCallback(string key, object value, CacheItemRemovedReason reason)
        {
            if (reason == CacheItemRemovedReason.Expired)
                Cache(key, DataGrabber.GrabHtml(key));
        }

        /// <summary>
        /// Return cached data by key
        /// </summary>
        /// <param name="key">Url</param>
        /// <returns>Html document</returns>
        public static HtmlDocument GetCachedData(string key)
        {
            HtmlDocument document;
            document = _cache[key] as HtmlDocument;
            if (document == null)
            {
                Cache(key,DataGrabber.GrabHtml(key));
                document = _cache[key] as HtmlDocument;
            }
            return document;
        }
    }

  


}