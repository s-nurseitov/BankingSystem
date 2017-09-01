using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public class NotificationCommand : Command
    {
        public override string Name => "notification";

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            //TODO: Command logic -_-

            client.SendTextMessageAsync(chatId, String.Format("SomeNotification"), replyToMessageId: messageId);
        }
    }
}