using OmniePDV.ReceiptSender.Core.Entities.Base;

namespace OmniePDV.ReceiptSender.Core.Entities;

public sealed class SaleProduct : Entity
{
    public int Order { get; set; }
    public double Quantity { get; set; }
    public Product Product { get; set; }
    public bool Deleted { get; set; }
}
