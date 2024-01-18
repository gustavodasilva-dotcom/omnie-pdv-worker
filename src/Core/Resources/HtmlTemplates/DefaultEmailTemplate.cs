namespace OmniePDV.ReceiptSender.Core.Resources.HtmlTemplates;

public static class DefaultEmailTemplate
{
    public static string HtmlText { get; } =
        @"
            <body style=""font-family: 'Arial', sans-serif; background-color: #f5f5f5; margin: 0; padding: 0;"">
              <div style=""max-width: 600px; margin: 20px auto; background-color: #fff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);"">
                
                [[custom-content]]

                <p style=""color: #555;"">Best regards,<br>
                The OmniePDV Team</p>
              </div>
            </body>
        ";
}
