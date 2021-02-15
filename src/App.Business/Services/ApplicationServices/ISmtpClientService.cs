using System.Threading.Tasks;

namespace App.Business.Services.ApplicationServices
{
    public interface ISmtpClientService
    {
        Task SendEmailAsync(string emails, string subject, string messages);
    }
}
