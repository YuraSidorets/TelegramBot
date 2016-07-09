using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.DataHelpers;

namespace TelegramBot.BotLogic
{
    public class Handler
    {
        private readonly TelegramBotClient _bot;

        public Handler()
        {
            _bot = Bot.Get();
        }

        public async void Handle(Message message)
        {
            if (message == null || message.Type != MessageType.TextMessage) return;

            if (message.Text.StartsWith("/start"))
            {
                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
               
                await
                    _bot.SendTextMessageAsync(message.Chat.Id,
                        "Hello, I'm Stoned Jesus Bot! :3 What you wanna from me?! See /help",
                        replyMarkup: new ForceReply());
            }

            else if (message.Text.StartsWith("/ITC"))
            {

                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await _bot.SendTextMessageAsync(message.Chat.Id, ITC.GetNews(),
                    replyMarkup: new ForceReply(),disableWebPagePreview:true);

            }
            else if (message.Text.StartsWith("/Habr"))
            {

                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await _bot.SendTextMessageAsync(message.Chat.Id, Habr.GetNews(),
                    replyMarkup: new ForceReply(), disableWebPagePreview: true);

            }
            else if (message.Text.StartsWith("/help"))
            {
                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await
                    _bot.SendTextMessageAsync(message.Chat.Id,
                        @"Usage:
/ITC - Latest news from ITC UA
/Habr - Latest news from Habrahabr ^^
/Recode - Latest news from Recode
/Flickr - Random photo from Flickr Explore
/Lounge - Nearest places to lounge
",
                        replyMarkup: new ForceReply());
            }
            else
            {
                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await
                    _bot.SendTextMessageAsync(message.Chat.Id,
                        "Command don't recognized, see /help",
                        replyMarkup: new ForceReply());
            }
        }

    }
}