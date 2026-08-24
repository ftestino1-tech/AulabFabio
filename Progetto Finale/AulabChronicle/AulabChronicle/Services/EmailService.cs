using System.Net; 
using System.Net.Mail; 

namespace AulabChronicle.Services; 

public class EmailService : IEmailService
{
    private readonly IConfiguration configuration; 

    public EmailService(IConfiguration configuration)
    {
        this.configuration = configuration; 
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mailHost = configuration["Email:Host"];
        var mailPort = int.Parse(configuration["Email:Port"] ?? "2525");
        var mailUser = configuration["Email:User"];
        var mailPass = configuration["Email:Pass"];

        using var client = new SmtpClient(mailHost, mailPort)
        {
            Credentials = new NetworkCredential(mailUser, mailPass), 
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("aulabpost@administration.com"), 
            Subject = subject, 
            Body = body, 
            IsBodyHtml = true
        };

        mailMessage.To.Add(to); 

        await client.SendMailAsync(mailMessage); 
    }
}