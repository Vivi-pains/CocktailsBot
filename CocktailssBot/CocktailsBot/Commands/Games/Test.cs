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
using Telegram.Bot.Types.ReplyMarkups;

namespace CocktailsBot.Commands
{
    class Test : Command
    {
        private readonly ApiClient _cocktailsClient = new ApiClient();
        public override TelegramBotClient Bot { get; set; }
        public override string Name { get; set; } = "Tests";
        public override bool IsWorking { get; set; } = false;
        public override int mes { get ; set ; }

        public string Answear;

        public Test(TelegramBotClient _bot)
        {
            Bot = _bot;
        }
        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {

            var run = new Random();
            int n = run.Next(0, 5);
            
            string name, data;
            
            var cocktail = _cocktailsClient.GetRandomCocktail().Result;
            Answear = cocktail.drinks[0].strDrink;

            List<InlineKeyboardButton[]> list = new List<InlineKeyboardButton[]>();
            for(int i = 0; i<5; i++)
            {
                if (i == n)
                {
                    name = Answear;

                    data = name + "(answer)";
                }
                else
                {
                    var _cocktail = _cocktailsClient.GetRandomCocktail().Result;
                    if(Answear == _cocktail.drinks[0].strDrink)
                    {
                        i -= 1;
                        continue;
                    }
                    name = _cocktail.drinks[0].strDrink;
                    data = Answear + '|' + name;

                }

                InlineKeyboardButton button = new InlineKeyboardButton() { CallbackData = "(test)"+data, Text = name };
                InlineKeyboardButton[] row = new InlineKeyboardButton[1] { button };
                list.Add(row);
            }

            var inlineKeyboard = new InlineKeyboardMarkup(list);

            await Bot.SendPhotoAsync(e.From.Id, cocktail.drinks[0].strDrinkThumb, replyMarkup: inlineKeyboard);

            EndComand();
        }

        public override async void EndComand()
        {
            IsWorking = false;
        }
    }
}
