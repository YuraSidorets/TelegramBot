using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TelegramBot.BotLogic;

namespace TelegramBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Bot.Get();
        }
    }
}
