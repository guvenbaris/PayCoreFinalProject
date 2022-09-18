using System.Net.Mail;

namespace PayCore.Application.Interfaces.Mail
{
    public interface ISmtpServer
    {
        SmtpClient GetSmtpClient();
    }
}
