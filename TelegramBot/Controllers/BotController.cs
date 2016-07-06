using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using TelegramBot.BotLogic;

namespace TelegramBot.Controllers
{
    public class BotController : ApiController
    {
        public OkResult Post(Update update)
        {
            Task.Run(() => new Handler().Handle(update.Message));
            return Ok();
        }
    }
}
