using System.Net;
using System.Net.Mail;
using DPS.Email.Helpers;

namespace DPS.Email;

public class EmailSender(EmailConfig config, EmailBodyFactory emailBodyFactory)
{
    public async Task SendResetPasswordEmail(string to, string resetPasswordUrl)
    {
        string subject = SubjectFactory.GetResetPasswordSubject();
        string body = await emailBodyFactory.RenderConfirmAccountEmailAsync(resetPasswordUrl);
        
         SendEmail(to, subject, body);
    }

    public async Task SendConfirmAccountEmail(string to, string confirmAccountUrl)
    {
        string subject = SubjectFactory.GetConfirmAccountSubject();
        string body = await emailBodyFactory.RenderConfirmAccountEmailAsync(confirmAccountUrl);
        
        SendEmail(to, subject, body);
    }
    private void SendEmail(string toAddress, string subject, string body)
    {
        using var client = GetSmtpClient();
        using var message = new MailMessage(
            from: new MailAddress(config.FromAddress, config.FromName),
            to: new MailAddress(toAddress)
        );
        message.Subject = subject;
        message.Body = body;

        client.Send(message);
    }

    private SmtpClient GetSmtpClient()
    {
        return new SmtpClient()
        {
            Host = config.Host,
            Port = config.Port,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            EnableSsl = true,
            Credentials = new NetworkCredential(config.Username, config.Password)
        };
    }
}