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
    class Start : Command
    {
        public override TelegramBotClient Bot { get; set; }
        public override string Name { get; set; } = "/start";
        public override bool IsWorking { get; set; } = false;
        public override int mes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> commands)
        {
            Bot = _bot;

            var replykeyboard_start = new ReplyKeyboardMarkup(Tables.main_menu, true, true);
            await Bot.SendTextMessageAsync(e.From.Id, Tables.start_text, replyMarkup: replykeyboard_start);
            EndComand();
        }

        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}
