using System;
using System.Dynamic;
using Telegram.Bot;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.ConnectBot();
        }
    }
}
