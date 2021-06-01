using CocktailsApi.Models;
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
    class MyFavorites : Command
    {
        private readonly ApiClient _cocktailsClient = new ApiClient();

        public IEnumerable<Favorites> Favor;
        public override TelegramBotClient Bot { get; set; }
        public override bool IsWorking { get; set; } = false;
        public override string Name { get; set; } = "My Favorites";
        public override int mes { get; set; }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            Bot = _bot;

            mes = e.From.Id;

            var Favor = await _cocktailsClient.GetFavorites(mes);

            List<InlineKeyboardButton[]> list = new List<InlineKeyboardButton[]>();
            foreach (Favorites cocktail in Favor)
            {
                InlineKeyboardButton button = new InlineKeyboardButton() { CallbackData = cocktail.CocktailName, Text = cocktail.CocktailName };
                InlineKeyboardButton[] row = new InlineKeyboardButton[1] { button };
                list.Add(row);
            }

            var inlineKeyboard = new InlineKeyboardMarkup(list);

            await Bot.SendTextMessageAsync(mes, "That's your favorites:", replyMarkup: inlineKeyboard);

            EndComand();
        }

        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}
