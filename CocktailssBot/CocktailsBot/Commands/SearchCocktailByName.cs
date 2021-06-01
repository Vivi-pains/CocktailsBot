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
using CocktailsBot;

namespace CocktailsBot.Commands
{
    public class SearchCocktailByName : Command
    {
        private readonly ApiClient _cocktailsClient = new ApiClient();

        public static List<Command> commands;

        public override string Name { get; set; } = "Знайти коктейль за назвою";
        public override TelegramBotClient Bot { get; set; }
        private InlineKeyboardMarkup inlineKeyboard { get; set; }
        private string CocktailName { get; set; }
        public override bool IsWorking { get; set; } = false;
        public override int mes { get; set; }

        public SearchCocktailByName(TelegramBotClient _bot)
        {
            Bot = _bot;
        }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            commands = _commands;
            IsWorking = true;

            mes = e.From.Id;

            Bot.OnMessage += GetString;

            await Bot.SendTextMessageAsync(e.From.Id, "Enter the name of the cocktail");
        }

        private async void GetString(object sender, MessageEventArgs e)
        {
            CocktailName = e.Message.Text;

            foreach (Command command in commands)
            {
                if (CocktailName == command.Name)
                {
                    await Bot.SendTextMessageAsync(mes, "Enter the name of the cocktail");
                    return;
                }
            }

            var cocktails = _cocktailsClient.GetCocktailByName(CocktailName).Result;

            SendInf(cocktails, e.Message);

            Bot.OnMessage -= GetString;
        }
        protected async void SendInf(Cocktails cocktails, Message e)
        {
            if (cocktails != null && cocktails.drinks != null)
            {
                List<InlineKeyboardButton[]> list = new List<InlineKeyboardButton[]>();
                foreach (Parametrs obj in cocktails.drinks)
                {
                    InlineKeyboardButton button = new InlineKeyboardButton() { CallbackData = obj.strDrink, Text = obj.strDrink };
                    InlineKeyboardButton[] row = new InlineKeyboardButton[1] { button };
                    list.Add(row);
                }

                inlineKeyboard = new InlineKeyboardMarkup(list);

                await Bot.SendTextMessageAsync(e.From.Id, "That's what we found:", replyMarkup: inlineKeyboard);
                EndComand();
            }
            else if (cocktails != null)
            {
                await Bot.SendTextMessageAsync(e.From.Id, $"<i>Sorr, but we didn'nt found any information about \"{CocktailName}\"</i>", parseMode: ParseMode.Html);
                EndComand();
            }
        }

        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}
