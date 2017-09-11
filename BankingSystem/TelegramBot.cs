using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BankingSystem
{
    public class TelegramBot
    {
        DatabaseManagementSystem database = new DatabaseManagementSystem();
        public async void Start_Bot()
        {
            var key = "422108608:AAEKs1HE_ZiXfl3okH3OctIACLCAX6q1eOY"; // получаем ключ из аргументов
            try
            {
                var Bot = new Telegram.Bot.TelegramBotClient(key); // инициализируем API
                await Bot.SetWebhookAsync(""); // Обязательно! убираем старую привязку к вебхуку для бота

                Bot.OnUpdate += async (object su, Telegram.Bot.Args.UpdateEventArgs evu) =>
                {
                    if (evu.Update.CallbackQuery != null || evu.Update.InlineQuery != null) return; // в этом блоке нам келлбэки и инлайны не нужны
                    var update = evu.Update;
                    var message = update.Message;
                    if (message == null) return;
                    if (message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
                    {
                        if (message.Text == "/hello")
                        {
                            // в ответ на команду /saysomething выводим сообщение
                            await Bot.SendTextMessageAsync(message.Chat.Id, "Приветствуем вас в наешм банке!", replyToMessageId: message.MessageId);
                        }
                        
                        if (message.Text == "/notification")
                        {
                            User user = database.FindPhoneNumber(message.Contact.PhoneNumber);
                            
                            if (user != null)
                            {
                                foreach (var str in user.lastOperations)
                                {
                                    await Bot.SendTextMessageAsync(message.Chat.Id, str, replyToMessageId: message.MessageId);
                                }
                                user.lastOperations.Clear();
                            }
                            
                        }
                    }
                };
               
                // запускаем прием обновлений
                Bot.StartReceiving();
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException ex)
            {
                Console.WriteLine(ex.Message); // если ключ не подошел - пишем об этом в консоль отладки
            }

        }

        private void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
