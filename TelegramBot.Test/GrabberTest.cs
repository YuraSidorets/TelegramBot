using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelegramBot.DataHelpers;
using TelegramBot.Utils;

namespace TelegramBot.Test
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void ITCTest()
        {
            var news =  Task.Factory.StartNew(Recode.GetNews).Result;

            Assert.IsNotNull(news);
        }

        [TestMethod]
        public void CacheTest()
        {
            string itc = "http://itc.ua/";
            DataCache.Cache(itc,DataGrabber.GrabHtml(itc));

            var cachedData = DataCache.GetCachedData(itc);

            Assert.IsNotNull(cachedData);
        }
    }
}
