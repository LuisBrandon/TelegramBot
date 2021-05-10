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
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Class
{
    class DatabaseBotTelegram
    {
        //Here we create the connectionString and the database connection
        static private string connectionString = "datasource=127.0.0.1;port=3306;username=Luigi;password=5231798610Po;database=telegrambot;";
        MySqlConnection databaseConnection = new MySqlConnection(connectionString);
        MySqlDataReader reader;
        

        public async Task<bool> checkIfuserisOndatabase(int userId)
        {
            //Select the user from the database
            string selectQuery = $"SELECT userid FROM user WHERE userid='{userId}'";
            

            try
            {
                MySqlCommand commandDatabase = new MySqlCommand(selectQuery,databaseConnection);
                commandDatabase.CommandTimeout = 30;
                //Opening the connection
                databaseConnection.Open();

                //trying to read the data with the selectQuery
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    //If we found results, we close both connections and returns true to confirm that there is a user with that id
                    reader.Close();
                    databaseConnection.Close();
                    return true;
                    
                }
                else
                {
                    //If we can't found any results, we will insert the userid into the user table. After that, we close the both connections
                   insertUserIdAsync(userId);
                   reader.Close();
                   databaseConnection.Close();
                   
                }


            }
            catch (Exception ex)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                databaseConnection.Close();

            }

            return false;

        }

        private async void insertUserIdAsync(int userId)
        {

            string insertUserQuery = $"INSERT INTO user (userid) VALUES ({userId});";
            MySqlCommand commandInsert = new MySqlCommand(insertUserQuery, databaseConnection);
            //Need to close the reader first because this method is called with the reader already open
            reader.Close();

            try
            {
                
                commandInsert.CommandTimeout = 30;
                reader = commandInsert.ExecuteReader();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"New user in 'user' table! With id:{userId}");
                Console.ResetColor();
                reader.Close();

            }
            catch (Exception ex)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

            }

        }



    }
}
