using OmniePDV.ReceiptSender.Worker.Entities;

namespace OmniePDV.ReceiptSender.Worker.Data.Entities;

public sealed class Sale(
    long Number,
    double Subtotal,
    double Total,
    Client Client,
    List<SaleProduct> Products) : Entity(Guid.NewGuid())
{
    public long Number { get; private set; } = Number;
    public double Subtotal { get; private set; } = Subtotal;
    public Discount? Discount { get; private set; }
    public double Total { get; private set; } = Total;
    public Client Client { get; private set; } = Client;
    public List<SaleProduct> Products { get; private set; } = Products;
    public SaleStatusEnum Status { get; private set; } = SaleStatusEnum.Open;
    public DateTime SaleDate { get; private set; } = DateTime.Now;

    public void UpdateTotal() => Total = Subtotal - (Discount?.Value ?? 0);

    public void UpdateSubtotal()
    {
        Subtotal = Products
            .Where(p => !p.Deleted)
            .Sum(p => p.Product.RetailPrice * p.Quantity);
        UpdateTotal();
    }

    public void SetClient(Client client) => Client = client;

    public void AddProduct(SaleProduct product) 
    {
        Products.Add(product);
        UpdateSubtotal();
    }

    public void AddDiscount(double discount, DiscountTypeEnum discountType = DiscountTypeEnum.Monetary)
    {
        double _discount = discount;
        
        if (discountType == DiscountTypeEnum.Percentage)
            _discount = Subtotal / discount;
        if (Discount == null)
            Discount = new Discount(discountType, _discount);
        else {
            Discount.SetValue(_discount);
            Discount.SetType(discountType);
        }        
        UpdateTotal();
    }

    public void CloseSale()
    {
        if (Status != SaleStatusEnum.Open)
            throw new Exception("It's not possible to close an already closed sale");
        Status = SaleStatusEnum.Closed;
    }
    public void CancelSale()
    {
        if (Status != SaleStatusEnum.Open)
            throw new Exception("It's not possible to cancel an already closed sale");
        Status = SaleStatusEnum.Cancelled;
    }
}
