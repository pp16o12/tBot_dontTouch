using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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
            botClient.OnCallbackQuery += BotClient_OnCallbackQuery;
            botClient.StartReceiving();
            Console.ReadLine();
        }

        static int[] messId = new int[3];

        private static async void BotClient_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            InlineKeyboardButton back = new InlineKeyboardButton();
            back.Text = "Вернутся";
            back.CallbackData = "/back";
            //int[] messId = new int[2];
            switch (e.CallbackQuery.Data.ToLower())
            {
                case "/taverna":
                    {
                        try
                        {
                            await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "https://pm1.narvii.com/6752/d77608881868568fdcfa363b3543626dcbf6cad2v2_hq.jpg", "Добро пожаловать!");
                            messId[0] = e.CallbackQuery.Message.MessageId;
                            await botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Куда идём?", replyMarkup: new InlineKeyboardMarkup(back));
                            messId[1] = e.CallbackQuery.Message.MessageId;
                            await deleteMessage(e.CallbackQuery.Message.Chat, messId);
                        }
                        catch(Telegram.Bot.Exceptions.ApiRequestException ex)
                        {
                            
                        }
                    }
                    break;
                case "/city":
                    {
                        try
                        {
                            await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "http://pro.radiomayak.ru/wp-content/uploads/2017/04/gorod1.jpg", "Славный город!");
                            messId[0] = e.CallbackQuery.Message.MessageId;
                            await botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Куда идём?", replyMarkup: new InlineKeyboardMarkup(back));
                            messId[1] = e.CallbackQuery.Message.MessageId;
                            await deleteMessage(e.CallbackQuery.Message.Chat, messId);
                        }
                        catch (Telegram.Bot.Exceptions.ApiRequestException ex)
                        {
                            
                        }
                    }
                    break;
                case "/hall":
                    {
                        try
                        {
                            await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "http://wallpaper-yaport.ru/baza/2010/05/14/89009309d0bf2ab32c79732abcd6738b.jpg", "Городская ратуша");
                            messId[0] = e.CallbackQuery.Message.MessageId;
                            await botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Куда идём?", replyMarkup: new InlineKeyboardMarkup(back));
                            messId[1] = e.CallbackQuery.Message.MessageId;
                            await deleteMessage(e.CallbackQuery.Message.Chat, messId);
                        }
                        catch (Telegram.Bot.Exceptions.ApiRequestException ex)
                        {

                        }
                    }
                    break;
                case "/back":
                    {
                        messId[2] = e.CallbackQuery.Message.MessageId;
                        await getMainGamePage(e.CallbackQuery.Message.Chat);
                    }
                    break;
                default:
                    break;
            }
        }

        private static async Task getMainGamePage(Chat userChat)
        {
            InlineKeyboardButton[] buttons = new InlineKeyboardButton[3];
            InlineKeyboardButton taverna = new InlineKeyboardButton();
            taverna.Text = "В таверну";
            taverna.CallbackData = "/taverna";
            InlineKeyboardButton city = new InlineKeyboardButton();
            city.Text = "В город";
            city.CallbackData = "/city";
            InlineKeyboardButton hall = new InlineKeyboardButton();
            hall.Text = "В ратушу";
            hall.CallbackData = "/hall";

            buttons[0] = taverna;
            buttons[1] = city;
            buttons[2] = hall;
            var klava = new InlineKeyboardMarkup(buttons);
            await botClient.SendTextMessageAsync(userChat.Id, "Куда идём?", replyMarkup: klava);
        }

        private async static void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.WriteLine(e.Message.Chat.Id + ":" + e.Message.Chat.Username + "= " + e.Message.Text);
            switch (e.Message.Text)
            {
                case "/start":
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Welcome");
                    //await botClient.SendPhotoAsync(e.Message.Chat.Id, "https://ru.clipartlogo.com/istockphoto/file?id=83234881&function=premium_detail&location=preview");
                    await getMainGamePage(e.Message.Chat);
                    break;
                default:
                    break;
            }
        }
        private static async Task deleteMessage(Chat chatId, int[] messId)
        {
            for (int i = 0; i < messId.Length; i++)
            {
                if(messId[i] != 0)
                    await botClient.DeleteMessageAsync(chatId, messId[i]);
            }
        }
    }
}
