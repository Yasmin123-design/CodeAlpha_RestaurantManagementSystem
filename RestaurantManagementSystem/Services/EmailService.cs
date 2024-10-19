using System.Net.Mail;
using System.Net;

namespace RestaurantManagementSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;


        public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task SendEmailAsyn(string email, string subject, string message)
        {

            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    // who send email (from)
                    From = new MailAddress(_smtpUser),
                    // resert password
                    Subject = subject,
                    // link message
                    Body = message,
                    IsBodyHtml = true,
                };
                // mail message contain all information about send from / send to / supject of message / which message send
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
