using System;
using System.Net;
using System.Net.Mail;

namespace BankingSystem
{
    public class Notification : INotificationService
    {
        public long Id { get; set; }

        public Notification(Server server)
        {
            server.changeAccount += Notify;
        }
        public bool Notify(User user, string subject, string body)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("SDPBank.162@gmail.com");
            msg.To.Add(user.Email);
            msg.Subject = subject;
            msg.Body = body;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("SDPBank.162@gmail.com", "qwerty123*");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
            }
            catch (Exception)
            {
                // запись логов
                return false;
            }
            return true;
        }
    }
}