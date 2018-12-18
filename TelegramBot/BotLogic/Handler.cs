using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

using TelegramBot.Services;

namespace TelegramBot.BotLogic
{
    public class Handler
    {
        private readonly TelegramBotClient bot;

        private readonly IPlacesService herePlaces;

        private readonly INewsService newsService;

        private readonly IPhotoService photoService;

        public Handler()
        {
            bot = Bot.Get();
            herePlaces = new HerePlaces();
            newsService = new NewsFormer();
            photoService = new Flickr();
        }

        public async void Handle(Message message)
        {
            if (message == null)
            {
                return;
            }

            if (message.Type == MessageType.Location)
            {

                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                await bot.SendTextMessageAsync(message.Chat.Id, herePlaces.FindPlace(message.Location.Latitude, message.Location.Longitude),
                replyMarkup: new ForceReplyMarkup());
            }

            if (message.Text.StartsWith("/start"))
            {
                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await
                    bot.SendTextMessageAsync(message.Chat.Id,
                        "Hello, I'm Stoned Jesus Bot! What do you wanna from me?! See /help",
                        replyMarkup: new ReplyKeyboardRemove());
            }
            else if (message.Text.StartsWith("/ITC"))
            {

                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await bot.SendTextMessageAsync(message.Chat.Id, newsService.ITC(),
                    replyMarkup: new ReplyKeyboardRemove(), disableWebPagePreview: true);

            }
            else if (message.Text.StartsWith("/Habr"))
            {

                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await bot.SendTextMessageAsync(message.Chat.Id, newsService.Habr(),
                    replyMarkup: new ReplyKeyboardRemove(), disableWebPagePreview: true);

            }
            else if (message.Text.StartsWith("/Recode"))
            {

                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                var news = newsService.Recode();
                await bot.SendTextMessageAsync(message.Chat.Id, news,
                    replyMarkup: new ReplyKeyboardRemove(), disableWebPagePreview: true);

            }
            else if (message.Text.StartsWith("/Flickr"))
            {

                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await bot.SendTextMessageAsync(message.Chat.Id, photoService.GetPhoto(),
                    replyMarkup: new ReplyKeyboardRemove());

            }
            else if (message.Text.StartsWith("/Lounge"))
            {

                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await bot.SendTextMessageAsync(message.Chat.Id, "Send your location",
                    replyMarkup: new ReplyKeyboardRemove());

            }
            else if (message.Text.StartsWith("/Map"))
            {

                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await bot.SendTextMessageAsync(message.Chat.Id, herePlaces.GetMap(message.Text.Substring(message.Text.IndexOf("p", StringComparison.InvariantCulture) + 1).Replace("d", ".").Replace("k", ",")),
                    replyMarkup: new ReplyKeyboardRemove());

            }
            else if (message.Text.StartsWith("/support"))
            {
                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await bot.SendTextMessageAsync(message.Chat.Id, @"Dear user!
This bot is made on free time without any profit.
If you are liking this bot so far you should consider sending a donation.
Card details: __4567 8901 2345 6789__
For any questions you can raise an issue on [Github page](https://github.com/YuraSidorets/TelegramBot)
", parseMode: ParseMode.Markdown, replyMarkup: new ReplyKeyboardRemove());

            }
            else if (message.Text.StartsWith("/help"))
            {
                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await
                    bot.SendTextMessageAsync(message.Chat.Id,
                        @"Usage:
/ITC - Latest news from ITC UA
/Habr - Latest news from Habrahabr
/Recode - Latest news from Recode
/Flickr - Random photo from Flickr Explore
/Lounge - Nearest places to lounge
/support - Ways to contribute to project
",
                        replyMarkup: new ReplyKeyboardRemove());
            }
            //else if (message.Text.StartsWith("/send "))
            //{
            //    await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri("");
            //        var content = new FormUrlEncodedContent(new[]
            //        {
            //    new KeyValuePair<string, string>("message", message.From.FirstName + ": " + message.Text.Substring(message.Text.IndexOf(" ")) )
            //});
            //        var result = await client.PostAsync("/Home/SendMessage", content);
            //    }
            //}
            else
            {
                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                await
                    bot.SendTextMessageAsync(message.Chat.Id,
                        "Command doesn't recognized, see /help",
                        replyMarkup: new ReplyKeyboardRemove());
            }
        }

    }
}