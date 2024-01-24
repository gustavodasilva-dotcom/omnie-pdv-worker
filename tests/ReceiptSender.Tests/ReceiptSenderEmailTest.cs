using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OmniePDV.ReceiptSender.Core.Models.Options;
using OmniePDV.ReceiptSender.Core.Models.Resources;
using OmniePDV.ReceiptSender.Core.Resources.HtmlTemplates;
using OmniePDV.ReceiptSender.Core.Services;

namespace ReceiptSender.Tests;

public class ReceiptSenderEmailTest
{
    [Fact]
    public async Task Send_Welcome_Email()
    {
        MailSettingsOptions mailOptions = new()
        {
            NoReplySender = new()
            {
                Name = "No Reply | OmniePDV",
                Email = ""
            },
            SMTP = new()
            {
                Host = "",
                Port = 0,
                UserName = "",
                Password = ""
            }
        };

        Mock<IOptions<MailSettingsOptions>> mailOptionsMock = new();
        mailOptionsMock.Setup(s => s.Value).Returns(mailOptions);

        Mock<ILogger<EmailSenderService>> logger = new();

        EmailSenderService senderService = new(
            logger: logger.Object,
            options: mailOptionsMock.Object
        );

        string bodyTemplate = DefaultEmailTemplate.HtmlText;
        bodyTemplate = bodyTemplate.Replace("[[custom-content]]",
            WelcomeEmailTemplate.ContentTemplate);

        MailPerson recipient = new(
            name: "Gustavo Aquino da Silva",
            email: "gustavaquinooficial@gmail.com"
        );
        MailPerson sender = new(
            name: mailOptions.NoReplySender.Name,
            email: mailOptions.NoReplySender.Email
        );

        MailModel model = new()
        {
            Froms = [ sender ],
            Tos = [ recipient ],
            Subject = "Email test",
            Body =
            {
                HtmlBody = bodyTemplate
            }
        };
        await senderService.SendEmailAsync(model);
    }
}