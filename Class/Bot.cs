using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using MySqlX.XDevAPI.Relational;
using TelegramBot.Class;
using System.Net;


namespace TelegramBot
{
    class Bot
    {
        static private string tokenBot = "xxxxxxxx";
        static public TelegramBotClient botClient = new TelegramBotClient(tokenBot);
        static private DatabaseBotTelegram databaseBotTelegram = new DatabaseBotTelegram();

        public void ConnectBot()
        {
            try
            {
                //Connecting the bot with the Telegram API using our token access
                
                var botInfo = botClient.GetMeAsync().Result;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The bot is on!");

                //When a message is received we call 'OnMessage' function
                //Adding the methods to the botClient object 
                //botClient.OnMessage += OnMessage;
                botClient.OnMessage += OnStart;
                botClient.OnMessage += OnURL;

                

                botClient.StartReceiving();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("~~~~Press any key to stop the bot~~~~");
                Console.ResetColor();
                Console.ReadKey();
                botClient.StopReceiving();
            }
            catch (Exception e)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

            }
        }

        //Function when a user use the '/start' command. If it is the first time, the bot will add the telegram id to the database.
        static async void OnStart(object sender, MessageEventArgs e)
        {
            //Create a user with a constructor with id
            User userFromTelegram = new User(e.Message.From.Id);

            //If the message is onlt '/start'
            if(e.Message.Text == "/start")
            {
                if(await databaseBotTelegram.checkIfuserisOndatabase(userFromTelegram.getUserid()))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(e.Message.From.Id + " already on the database");
                    Console.ResetColor();
                    await botClient.SendTextMessageAsync(chatId:e.Message.Chat,text:$"Bienvenido de nuevo, {e.Message.From.FirstName} ;)");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("New user! " + "ID:"+e.Message.From.Id+" Username: " + e.Message.From.Username +" Name: " + e.Message.From.FirstName);
                    Console.ResetColor();
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: $"Bienvenido a MarketGazer, {e.Message.From.FirstName}!");
                }

            }

        }        


        //Function when a user sends a url to track it.
        static async void OnURL(object sender, MessageEventArgs e)
        {

            //////////////////////////// To do

        }

    }
}
