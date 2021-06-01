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
    class AlchoCrocodile : Command
    {
        public Dictionary<string, int> points;

        public List<Command> commands;

        private readonly ApiClient _cocktailsClient = new ApiClient();

        public override TelegramBotClient Bot { get ; set ; }
        public override string Name { get; set; } = "AlchoCrocodile";
        public override bool IsWorking { get; set; } = false;
        public override int mes { get; set; }

        public override async void Execute(TelegramBotClient _bot, Message _e, List<Command> _commands)
        {
            Bot = _bot;

            mes = _e.From.Id;

            commands = _commands;

            points = new Dictionary<string, int>();
            

            await Bot.SendTextMessageAsync(mes, Tables.rule_AlchoCrocodile);

            var replykeyboard = new ReplyKeyboardMarkup(Tables.EndAddingUser, true);
            await Bot.SendTextMessageAsync(mes, "Enter names of users one by one", replyMarkup: replykeyboard);

            Bot.OnMessage += GetString;
        }

        protected async void GetString(object sender, MessageEventArgs e)
        {
            string str = e.Message.Text;
            if (str != "End adding users" && str != "End game")
            {
                points.Add(str, 0);
            }
            else if (str == "End adding users")
            {
                if (points == null)
                {
                    await Bot.SendTextMessageAsync(e.Message.From.Id, "You didn't enter any user");
                    return;
                }
                var replykeyboard = new ReplyKeyboardMarkup(Tables.EndGame, true);
                await Bot.SendTextMessageAsync(e.Message.From.Id, "Now we can start the game", replyMarkup: replykeyboard);

                SendInf();
            } else if(str == "End game")
            {
                Bot.OnMessage -= GetString;
                EndComand();
            }
        }

        private async void ButtonPressed(object sender, CallbackQueryEventArgs e)
        {
            await Bot.EditMessageReplyMarkupAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId, null);

            Bot.OnCallbackQuery -= ButtonPressed;

            string user = e.CallbackQuery.Data.Replace("(game)", "");

            points[user]++;

            SendInf();
        }

        protected async void SendInf()
        {
            var cocktail = _cocktailsClient.GetRandomCocktail().Result;

            List<InlineKeyboardButton[]> list = new List<InlineKeyboardButton[]>();
            foreach (var line in points) 
            {
                InlineKeyboardButton button = new InlineKeyboardButton() { CallbackData = line.Key+"(game)", Text = line.Key };
                InlineKeyboardButton[] row = new InlineKeyboardButton[1] { button };
                list.Add(row);
            }

            var inlineKeyboard = new InlineKeyboardMarkup(list);

            await Bot.SendPhotoAsync(mes, cocktail.drinks[0].strDrinkThumb, caption:cocktail.drinks[0].Print_inf(), ParseMode.Html, replyMarkup: inlineKeyboard);

            Bot.OnCallbackQuery += ButtonPressed;
        }

        public override async void EndComand()
        {
            string schore = "<b>SCHORE</b>\n\n";
            foreach(var line in points)
            {
                schore += $"{line.Key}: {line.Value}\n";
            }

            await Bot.SendTextMessageAsync(mes, schore, ParseMode.Html);
            points = null;
            Bot.OnCallbackQuery -= ButtonPressed;

            var replykeyboard_start = new ReplyKeyboardMarkup(Tables.main_menu, true, true);
            await Bot.SendTextMessageAsync(mes, "Chose operation", replyMarkup: replykeyboard_start);

            IsWorking = false;
            return;
        }
    }
}
