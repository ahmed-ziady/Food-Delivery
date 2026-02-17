using FoodDelivery.Application.Common.Interfaces.Twilio;
using FoodDelivery.Infrastructure.Authentication.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.Options;
using MimeKit;

public sealed class EmailService(
    IOptions<EmailSettings> options,
    IHostEnvironment env) : IMailingService
{
    private readonly EmailSettings _settings = options.Value;
    private readonly IHostEnvironment _env = env;

    public async Task SendEmailAsync(string toEmail, string subject, string otpCode)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
            throw new ArgumentException("Recipient email is required.");

        var templatePath = Path.Combine(
            _env.ContentRootPath,
            "EmailTemplates",
            "OtpTemplate.html");

        if (!File.Exists(templatePath))
            throw new FileNotFoundException($"Email template not found: {templatePath}");

        var htmlTemplate = await File.ReadAllTextAsync(templatePath);
        var finalBody = htmlTemplate.Replace("{{OTP_CODE}}", otpCode);

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.DisplayName, _settings.Email));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new BodyBuilder { HtmlBody = finalBody }.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.Email, _settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
