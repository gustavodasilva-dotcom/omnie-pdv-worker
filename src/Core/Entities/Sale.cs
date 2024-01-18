using OmniePDV.ReceiptSender.Core.Entities.Base;
using OmniePDV.ReceiptSender.Core.Entities.Enums;

namespace OmniePDV.ReceiptSender.Core.Entities;

public sealed class Sale : Entity
{
    public long Number { get; set; }
    public double Subtotal { get; set; }
    public Discount? Discount { get; set; }
    public double Total { get; set; }
    public Client Client { get; set; }
    public List<SaleProduct> Products { get; set; }
    public SaleStatusEnum Status { get; set; }
    public DateTime SaleDate { get; set; }
}
