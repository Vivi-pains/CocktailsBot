using CocktailsApi.Models;
using CocktailsBot.Clients;
using CocktailsBot.Models;
using CocktailsBot;
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
    class SearchCocktailByIngridients : Command
    {
        private readonly ApiClient _cocktailsClient = new ApiClient();
        public override TelegramBotClient Bot { get; set; }
        public override string Name { get; set; } = "Знайти коктейль за інгрідієнтами";
        public string Ingridients { get; set; }
        public override bool IsWorking { get; set; } = false;
        public override int mes { get; set; }

        public static List<Command> commands;

        public SearchCocktailByIngridients(TelegramBotClient _bot)
        {
            Bot = _bot;
        }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            commands = _commands;

            mes = e.From.Id;

            await Bot.SendTextMessageAsync(mes, "Enter the comma-separated ingredients");

            Bot.OnMessage += GetString;
        }

        protected async void GetString(object sender, MessageEventArgs e)
        {            
            Ingridients = e.Message.Text;

            foreach (Command command in commands)
            {
                if (Ingridients == command.Name)
                {
                    await Bot.SendTextMessageAsync(mes, "Enter the comma-separated ingredients");
                    return;
                }
            }

            var cocktails = _cocktailsClient.GetCocktailsByIngridients(Ingridients).Result;

            SendInf(cocktails);

            Bot.OnMessage -= GetString;
        }

        protected async void SendInf(FilterCocktails cocktails)
        {
            if (cocktails != null && cocktails.drinks != null)
            {

                List<InlineKeyboardButton[]> list = new List<InlineKeyboardButton[]>();
                foreach (Drink obj in cocktails.drinks)
                {
                    InlineKeyboardButton button = new InlineKeyboardButton() { CallbackData = obj.strDrink, Text = obj.strDrink };
                    InlineKeyboardButton[] row = new InlineKeyboardButton[1] { button };
                    list.Add(row);
                }

                var inlineKeyboard = new InlineKeyboardMarkup(list);
                await Bot.SendTextMessageAsync(mes, "That's what we found:", replyMarkup: inlineKeyboard);
                EndComand();
            }
            else
            {
                await Bot.SendTextMessageAsync(mes, $"Sorry, but we didn't found any information about \"{Ingridients}\"", parseMode: ParseMode.Html);
                EndComand();
            }

        }
        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}
