using OmniePDV.ReceiptSender.Core.Entities.Base;

namespace OmniePDV.ReceiptSender.Core.Entities;

public sealed class Manufacturer : Entity
{
    public string Name { get; set; }
    public string CRN { get; set; }
    public bool Active { get; set; }
}
