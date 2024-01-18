using OmniePDV.ReceiptSender.Core.Entities.Enums;

namespace OmniePDV.ReceiptSender.Core.Entities;

public sealed class Discount
{
    public DiscountTypeEnum Type { get; private set; }
    public double Value { get; private set; }
}
