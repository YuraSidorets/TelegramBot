using System;
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
            if (message == null) return;

            if (message.Type == MessageType.LocationMessage)
            {

                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                    await _bot.SendTextMessageAsync(message.Chat.Id, GooglePlaces.FindPlace(message.Location.Latitude, message.Location.Longitude),
                    replyMarkup: new ForceReply());
            }

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

                await _bot.SendTextMessageAsync(message.Chat.Id,NewsFormer.ITC(),
                    replyMarkup: new ForceReply(),disableWebPagePreview:true);

            }
            else if (message.Text.StartsWith("/Habr"))
            {

                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await _bot.SendTextMessageAsync(message.Chat.Id, NewsFormer.Habr(),
                    replyMarkup: new ForceReply(), disableWebPagePreview: true);

            }
            else if (message.Text.StartsWith("/Recode"))
            {

                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await _bot.SendTextMessageAsync(message.Chat.Id, NewsFormer.Recode(),
                    replyMarkup: new ForceReply(), disableWebPagePreview: true);

            }
            else if (message.Text.StartsWith("/Flickr"))
            {

                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await _bot.SendTextMessageAsync(message.Chat.Id, Flickr.GetPhoto(),
                    replyMarkup: new ForceReply());

            }
            else if (message.Text.StartsWith("/Lounge"))
            {

                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await _bot.SendTextMessageAsync(message.Chat.Id, "Send your location",
                    replyMarkup: new ForceReply());

            }
            else if (message.Text.StartsWith("/Map"))
            {

                await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await _bot.SendTextMessageAsync(message.Chat.Id, GooglePlaces.GetMap(message.Text.Substring(message.Text.IndexOf("p",StringComparison.CurrentCulture)+1).Replace("d",".").Replace("k", ",")),
                    replyMarkup: new ForceReply());

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