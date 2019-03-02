using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ConsoleApp1
{
    class Program
    {
        static TelegramBotClient botClient;
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("742631594:AAE5uxvJTix99mbS6-HK3WpKS8ndAvaIGk4");
            var bot = botClient.GetMeAsync().Result;
            Console.WriteLine(bot.Username);

            botClient.OnMessage += BotClient_OnMessage;
            botClient.StartReceiving();
            Console.ReadLine();
        }

        private static void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.WriteLine(e.Message.Chat.Id + ":" + e.Message.Chat.Username + "= " + e.Message.Text);
            switch (e.Message.Text)
            {
                case "/start":
                    botClient.SendTextMessageAsync(e.Message.Chat.Id, "Welcome");
                    //botClient.SendPhotoAsync(e.Message.Chat.Id, "C:/Users/student/Desktop/83234881-medieval-game-sprites-characters-collection-arbalester.jpg");
                    break;
                default:
                    break;
            }
        }
    }
}
