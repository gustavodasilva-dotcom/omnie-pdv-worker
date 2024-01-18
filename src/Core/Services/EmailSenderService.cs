using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using OmniePDV.ReceiptSender.Core.Contracts.Services;
using OmniePDV.ReceiptSender.Core.Models.Options;
using OmniePDV.ReceiptSender.Core.Models.Resources;

namespace OmniePDV.ReceiptSender.Core.Services;

public sealed class EmailSenderService(
	ILogger<EmailSenderService> logger,
	IOptions<MailSettingsOptions> options) : IEmailSenderService
{
	private readonly ILogger<EmailSenderService> _logger = logger;
    private readonly MailSettingsOptions _options = options.Value;

    public async Task SendEmailAsync(MailModel model)
    {
		try
		{
			using MimeMessage message = new();

			model.Froms.ForEach(from =>
			{
                message.From.Add(new MailboxAddress(
					name: from.Name,
					address: from.Email
				));
            });
            model.Tos.ForEach(to =>
            {
                message.To.Add(new MailboxAddress(
                    name: to.Name,
                    address: to.Email
                ));
            });
            model.Ccs.ForEach(cc =>
            {
                message.Cc.Add(new MailboxAddress(
                    name: cc.Name,
                    address: cc.Email
                ));
            });
            model.Bccs.ForEach(bcc =>
            {
                message.Bcc.Add(new MailboxAddress(
                    name: bcc.Name,
                    address: bcc.Email
                ));
            });

            message.Subject = model.Subject;

            string body = model.Body.HtmlBody;
            foreach (KeyValuePair<string, string> param in model.Body.Parameters)
                body = body.Replace(param.Key, param.Value);
            BodyBuilder builder = new()
            {
                HtmlBody = body
            };
            message.Body = builder.ToMessageBody();

            using SmtpClient mailClient = new();
            mailClient.Connect(
                host: _options.SMTP.Host,
                port: _options.SMTP.Port,
                MailKit.Security.SecureSocketOptions.StartTls
            );
            mailClient.Authenticate(
                userName: _options.SMTP.UserName,
                password: _options.SMTP.Password
            );
            await mailClient.SendAsync(message);
            await mailClient.DisconnectAsync(true);
        }
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while configuring/sending the email");
		}
    }
}
