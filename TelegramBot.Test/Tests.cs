using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelegramBot.DataHelpers;
using TelegramBot.Utils;

namespace TelegramBot.Test
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GetNewsTest()
        {
            var news =  Task.Factory.StartNew(NewsFormer.ITC).Result;

            Assert.IsNotNull(news);
        }

        [TestMethod]
        public void GetPhotoTest()
        {
            var news = Task.Factory.StartNew(Flickr.GetPhoto).Result;

            Assert.IsNotNull(news);
        }

        [TestMethod]
        public void CacheTest()
        {
            string itc = "https://www.flickr.com/explore/";
            //DataCache.Cache(itc,DataGrabber.GrabHtml(itc));

            var cachedData = DataCache.GetCachedData(itc);

            Assert.IsNotNull(cachedData);
        }

        [TestMethod]
        public void MapsTest()
        {
            var data = GooglePlaces.FindPlace(50.496781, 30.761194);

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void GetMapTest()
        {
            var data = GooglePlaces.GetMap("50.4517896,30.4686003");

            Assert.IsNotNull(data);
        }


    }
}
