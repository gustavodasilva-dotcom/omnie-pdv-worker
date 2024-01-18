using OmniePDV.ReceiptSender.Core.Entities.Base;

namespace OmniePDV.ReceiptSender.Core.Entities;

public sealed class Client : Entity
{
    public string Name { get; set; }
    public string SSN { get; set; }
    public DateTime Birthday { get; set; }
    public string? Email { get; set; }
    public bool Active { get; set; }
}
