using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace CocktailsBot.Models
{
    class Tables
    {
        public static KeyboardButton[][] EndAddingUser = new[]
        {
            new[]
            {
                new KeyboardButton("End adding users")
            }
        };

        public static KeyboardButton[][] EndGame = new[]
        {
            new[]
            {
                new KeyboardButton("End game")
            }
        };

        public static KeyboardButton[][] main_menu = new[]
        {
            new[]
            {
                new KeyboardButton("Знайти коктейль за назвою"),
                new KeyboardButton("Знайти коктейль за інгрідієнтами")
            },
            new[]
            {
                new KeyboardButton("Рандомний коктейль"),
                new KeyboardButton("My Favorites")
            },
            new[]
            {
                new KeyboardButton("Mini Games")
            }
        };

        public static KeyboardButton[][] game_menu = new[]
        {
            new[]
            {
                new KeyboardButton("Tests"),
                new KeyboardButton("AlchoCrocodile")
            },
            new[]
            {
                new KeyboardButton("<<Back<<")
            }
        };
        //public static InlineKeyboardButton[][] empty;

        public static string start_text = "Привіт! Ти щойно розпочав роботу з " +
                                    "котейльним ботом. Я можу допомогти тобі " +
                                    "вибрати напій за твоїм смаком, а також трішки розважитись";

        public static string rule_AlchoCrocodile = "Тут будуть правила гри, але їх я напишу пізніше <3 і початкові інструкції";
    }
}
