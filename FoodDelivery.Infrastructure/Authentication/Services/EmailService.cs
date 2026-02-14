using FoodDelivery.Application.Common.Interfaces.Twilio;
using FoodDelivery.Infrastructure.Authentication.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FoodDelivery.Infrastructure.Authentication.Services;


public sealed class EmailService(IOptions<EmailSettings> options) : IMailingService
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendEmailAsync(
        string toEmail,
        string subject,
        string otpCode)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
            throw new ArgumentException("Recipient email is required.");

        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(
            _settings.DisplayName,
            _settings.Email));

        email.To.Add(MailboxAddress.Parse(toEmail));

        email.Subject = subject;

        var templatePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "EmailTemplates",
            "OtpTemplate.html");

        var htmlTemplate = await File.ReadAllTextAsync(templatePath);

        // 🔥 Replace placeholder
        var finalBody = htmlTemplate.Replace("{{OTP_CODE}}", otpCode);

        var builder = new BodyBuilder
        {
            HtmlBody = finalBody
        };

        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();

        try
        {
            await smtp.ConnectAsync(
                _settings.SmtpServer,
                _settings.SmtpPort,
                MailKit.Security.SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _settings.Email,
                _settings.Password);

            await smtp.SendAsync(email);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                "Failed to send email.",
                ex);
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}

