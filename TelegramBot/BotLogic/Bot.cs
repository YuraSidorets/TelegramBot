using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.BotLogic
{
    public static class Bot
    {
        private static TelegramBotClient _bot;

        public static TelegramBotClient Get()
        {
            if (_bot != null) return _bot;
            _bot = new TelegramBotClient(Config.BotApiKey);
            _bot.SetWebhookAsync(Config.WebHookUrl).Wait();
            return _bot;
        }
    }
}