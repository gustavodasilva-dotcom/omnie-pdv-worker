namespace OmniePDV.ReceiptSender.Core.Resources.HtmlTemplates;

public static class ReceiptEmailTemplate
{
    public static string ContentTemplate { get; } =
        @"
            <div style=""margin-top: 20px;"">
              <p><strong>Purchase details</strong></p>
              <p><strong>Order Number:</strong> [order-number]</p>
              <p><strong>Client:</strong> [order-client]</p>
              <p><strong>Items Purchased:</strong><br>
                [[custom-content]]
              </p>
              <p><strong>Total Amount:</strong> $[total-amount]</p>
            </div>
        ";

    public static string ItemTemplate { get; } = @" - [item-description]<br>";
}
