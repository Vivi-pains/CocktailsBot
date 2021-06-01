using CocktailsBot.Clients;
using CocktailsBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CocktailsBot.Commands
{
    class RandomCocktail : Command
    {
        private readonly ApiClient _cocktailsClient = new ApiClient();
        public override string Name { get; set; } = "Рандомний коктейль";
        public override TelegramBotClient Bot { get; set; }
        public override bool IsWorking { get; set; } = false;
        public override int mes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            Bot = _bot;

            var cocktails = _cocktailsClient.GetRandomCocktail().Result;

            InlineKeyboardButton[][] favourite_buttons = new[]
            {
                new[]
                {
                    new InlineKeyboardButton {CallbackData = "(like)"+cocktails.drinks[0].strDrink, Text = "❤️"},
                    new InlineKeyboardButton {CallbackData = "(dislike)"+cocktails.drinks[0].strDrink, Text = "💔"},
                }
            };

            var inlineKeyboard = new InlineKeyboardMarkup(favourite_buttons);

            await Bot.SendPhotoAsync(e.From.Id, photo: cocktails.drinks[0].strDrinkThumb, caption: cocktails.drinks[0].Print_inf(), parseMode: ParseMode.Html, replyMarkup: inlineKeyboard);

            EndComand();
        }

        public override void EndComand()
        {
            IsWorking = false;
        }

    }

}
