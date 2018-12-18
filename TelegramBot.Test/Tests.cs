using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TelegramBot.Services;
using TelegramBot.Utils;

namespace TelegramBot.Test
{
    [TestClass]
    public class Tests
    {
        private INewsService newsService;

        private IPhotoService photoService;

        private IPlacesService herePlaces;

        [TestMethod]
        public void GetNewsTest()
        {
            newsService = new NewsFormer();
            var news =  Task.Factory.StartNew(newsService.Recode).Result;

            Assert.IsNotNull(news);
        }

        [TestMethod]
        public void GetPhotoTest()
        {
            photoService = new Flickr();
            var news = Task.Factory.StartNew(photoService.GetPhoto).Result;

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
            herePlaces = new HerePlaces();
            var data = herePlaces.FindPlace(50.496781, 30.761194);

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void GetMapTest()
        {
            herePlaces = new HerePlaces();
            var data = herePlaces.GetMap("50.4517896,30.4686003");

            Assert.IsNotNull(data);
        }


    }
}
