using System.Web.Http;
using TelegramBot.BotLogic;

namespace TelegramBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Bot.Get();
        }
    }
}
