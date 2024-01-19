namespace OmniePDV.ReceiptSender.Core.Resources.HtmlTemplates;

public static class ReceiptEmailTemplate
{
    public static string ContentTemplate { get; } =
        @"
            <div style=""margin-top: 20px;"">
              <div style=""background-color: #005f73;"">
                <h2 style=""text-align: center; color: white; padding: 10px;"">Purchase details</h2>
              </div>
              <p>
                <strong>Order Number:</strong> <span style=""font-size: 25px; font-weight: bold; color: #ae2012;"">[order-number]</span>
              </p>
              <p>
                <strong>Client:</strong> [order-client]
              </p>
              <p>
                <strong>Items Purchased:</strong><br>
              </p>
              <div style=""margin-top: 10px;"">
                <table style=""width: 100%; border-collapse: collapse;"">
                  <tr>
                    <th style=""border: 1px solid #ddd; padding: 12px; text-align: left; background-color: #ca6702; color: white;"">Description</th>
                    <th style=""border: 1px solid #ddd; padding: 12px; text-align: left; background-color: #ca6702; color: white;"">Price</th>
                    <th style=""border: 1px solid #ddd; padding: 12px; text-align: left; background-color: #ca6702; color: white;"">Quantity</th>
                    <th style=""border: 1px solid #ddd; padding: 12px; text-align: left; background-color: #ca6702; color: white;"">Total Price</th>
                  </tr>
                  [[table-rows-content]]
                </table>
              </div>
              <div style=""display: flex; float: right; margin-top: 10px; margin-bottom: 50px; border:1px solid;"">
                <div style=""padding: 15px 10px 8px 30px; background-color: #94d2bd; font-weight: bold; display: flex;"">
                  <span style=""align-self: flex-end;"">Total Amount:</span>
                </div>
                <div style=""padding: 15px 10px 6px 30px; background-color: #e9d8a6; font-size: 25px; display: flex;"">
                  <span style=""align-self: flex-end;"">[total-amount]</span>
                </div>
              </div>
            </div>
        ";

    public static string TableRowTemplate { get; } =
        @"
            <tr>
              <td style=""text-align: center; border: 1px solid; padding: 8px;"">[item-description]</td>
              <td style=""text-align: center; border: 1px solid; padding: 8px;"">[item-unitary-price]</td>
              <td style=""text-align: center; border: 1px solid; padding: 8px;"">[item-quantity]</td>
              <td style=""text-align: center; border: 1px solid; padding: 8px;"">[item-total-price]</td>
            </tr>
        ";
}
