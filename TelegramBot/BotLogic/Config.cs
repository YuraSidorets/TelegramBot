using System.Collections.Specialized;
using System.Configuration;

namespace TelegramBot.BotLogic
{
    public static class Config
    {
        private static readonly NameValueCollection Appsettings = ConfigurationManager.AppSettings;

        public static string BotApiKey => Appsettings["170904116:AAHpil07V0xDdkrNCrNIySx8CEajntS2_lk"];

        public static string WebHookUrl => Appsettings[$"http://stoned-jesusbot.azurewebsites.net/"];
    }
}
