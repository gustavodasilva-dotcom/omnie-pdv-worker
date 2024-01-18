using OmniePDV.ReceiptSender.Core.Entities;

namespace OmniePDV.ReceiptSender.Core.Models.Payloads;

public sealed class SendReceiptEmailPlayload
{
    public Sale Sale { get; set; }
    public string Email { get; set; }
}
