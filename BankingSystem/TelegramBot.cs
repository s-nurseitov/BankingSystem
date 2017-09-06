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
        public List<string> lastOperations=new List<string>();
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
                        if (message.Text == "/saysomething")
                        {
                            // в ответ на команду /saysomething выводим сообщение
                            await Bot.SendTextMessageAsync(message.Chat.Id, "Приветствуем вас в наешм банке!", replyToMessageId: message.MessageId);
                        }
                        if (message.Text == "/notification")
                        {
                            foreach (var str in lastOperations) {
                                await Bot.SendTextMessageAsync(message.Chat.Id, str, replyToMessageId: message.MessageId);
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
    }
}
