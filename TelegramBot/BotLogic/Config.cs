using System.Collections.Specialized;
using System.Configuration;

namespace TelegramBot.BotLogic
{
    public static class Config
    {
        private static readonly NameValueCollection Appsettings = ConfigurationManager.AppSettings;

        public static string BotApiKey => Appsettings["BotApiKey"];

        public static string WebHookUrl => Appsettings["WebHookUrl"];
    }
}
