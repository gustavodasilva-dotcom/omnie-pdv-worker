using OmniePDV.ReceiptSender.Core.Models.Resources;

namespace OmniePDV.ReceiptSender.Core.Contracts.Services;

public interface IEmailSenderService
{
    Task SendEmailAsync(MailModel model);
}
