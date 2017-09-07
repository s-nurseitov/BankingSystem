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
        public void Notify(object obj)
        {
            Сontainer container = (Сontainer)obj;
            MailMessage msg = new MailMessage();
            try
            {
                msg.From = new MailAddress("SDPBank.162@gmail.com");
                msg.To.Add(container.user.Email);
                msg.Subject = container.subject;
                msg.Body = container.body;
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("SDPBank.162@gmail.com", "qwerty123*");
                client.Timeout = 20000;
                client.Send(msg);
            }
            catch (System.FormatException ex)
            {
                //Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
        }
    }
}