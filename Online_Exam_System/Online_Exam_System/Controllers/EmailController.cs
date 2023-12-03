using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Threading.Tasks;

public class EmailController : Controller
{
   // private readonly EmailService _emailService;

    public EmailController()
    {
      // _emailService = emailService;
    }

    public IActionResult Index()
    {
        return View();
    }
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Akash", "akash.ghosh1700@gmail.com"));
        message.To.Add(new MailboxAddress("Subrato", "subrato2393@gmail.com"));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = body
        };

        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("akash.ghosh1700@gmail.com", "hmhxrzzdebuibery");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
    [HttpGet]
    public async Task<IActionResult> SendEmail(string to, string subject, string body)
    {
        try
        {
            // You can validate inputs here if needed

            // await _emailService.SendEmailAsync(to, subject, body);

            await SendEmailAsync(to,subject,body);

            ViewBag.Message = "Email sent successfully!";
            return View();
        }
        catch (Exception ex)
        {
            // Handle exceptions and log errors
            ViewBag.Error = $"An error occurred: {ex.Message}";
            return View("Index");
        }
    }
}
