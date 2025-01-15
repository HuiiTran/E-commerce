using System.Net.Mail;
using static MassTransit.Monitoring.Performance.BuiltInCounters;
namespace UsersApi.Entities
{
    public class SendMail
    {
        public void SendMailTo(string senderAddress,string subject, string message)
        {
            var mailMessage = new MailMessage()
            {
                From = new MailAddress(senderAddress),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
        }
    }
}
