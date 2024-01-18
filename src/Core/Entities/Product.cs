using OmniePDV.ReceiptSender.Core.Entities.Base;

namespace OmniePDV.ReceiptSender.Core.Entities;

public sealed class Product : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double WholesalePrice { get; set; }
    public double RetailPrice { get; set; }
    public string Barcode { get; set; }
    public Manufacturer Manufacturer { get; set; }
    public bool Active { get; set; }
}
