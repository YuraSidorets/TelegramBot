using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelegramBot.Utils;

namespace TelegramBot.Test
{
    [TestClass]
    public class GrabberTest
    {
        [TestMethod]
        public void ParseHtmlTest()
        {
            var res =  ITC.GetNews();
        }
    }
}
