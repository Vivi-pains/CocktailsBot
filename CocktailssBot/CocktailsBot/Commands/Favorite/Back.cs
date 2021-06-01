using CocktailsBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CocktailsBot.Commands
{
    class Back : Command
    {
        public override TelegramBotClient Bot { get; set; }
        public override bool IsWorking { get; set; } = false;
        public override string Name { get; set; } = "<<Back<<";
        public override int mes { get ; set ; }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            Bot = _bot;

            mes = e.From.Id;

            var replykeyboard_start = new ReplyKeyboardMarkup(Tables.main_menu, true, true);
            await Bot.SendTextMessageAsync(mes, "Choose action", replyMarkup: replykeyboard_start);
            EndComand();
        }

        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}