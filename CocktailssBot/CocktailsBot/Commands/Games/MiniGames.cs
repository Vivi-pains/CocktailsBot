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
    class MiniGames : Command
    {
        public override TelegramBotClient Bot { get ; set ; }
        public override string Name { get; set; } = "Mini Games";
        public override bool IsWorking { get; set; } = false;
        public override int mes { get; set; }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> commands)
        {
            Bot = _bot;

            mes = e.From.Id;

            var replykeyboard_game = new ReplyKeyboardMarkup(Tables.game_menu, true);

            await Bot.SendTextMessageAsync(mes, "Choose a game", replyMarkup: replykeyboard_game);

            EndComand();
        }
        
        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}
