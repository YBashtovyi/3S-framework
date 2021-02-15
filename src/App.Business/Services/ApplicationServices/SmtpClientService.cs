using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace App.Business.Services.ApplicationServices
{
    public class SmtpClientService: ISmtpClientService
    {
        #region fields
        private readonly IConfiguration _configuration;
        #endregion

        #region constructor
        public SmtpClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region methods: public
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:SenderName"), _configuration.GetValue<string>("EmailSettings:Sender")));
            mimeMessage.To.Add(new MailboxAddress(email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("html")
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration.GetValue<string>("EmailSettings:MailServer"), _configuration.GetValue<int>("EmailSettings:MailPort"));
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
        #endregion
    }
}
