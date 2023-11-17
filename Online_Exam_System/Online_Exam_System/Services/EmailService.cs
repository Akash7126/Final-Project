using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

public class EmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Subrato", "subrato2393@gmail.com"));
        message.To.Add(new MailboxAddress("", "akash.ghosh1600@gmail.com"));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = body
        };

        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("subrato2393@gmail.com", "s78276820");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
