using Telegram.Bot;

namespace TelegramBot.BotLogic
{
    public static class Bot
    {
        private static TelegramBotClient _bot;

        public static TelegramBotClient Get()
        {
            if (_bot != null) return _bot;
            _bot = new TelegramBotClient(Config.BotApiKey);
            _bot.SetWebhookAsync(Config.WebHookUrl);
            return _bot;
        }
    }
}