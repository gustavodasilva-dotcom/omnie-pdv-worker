namespace OmniePDV.ReceiptSender.Core.Resources.HtmlTemplates;

public static class WelcomeEmailTemplate
{
    public static string ContentTemplate { get; } =
        @"
            <h1 style=""color: #333;"">Welcome to OmniePDV!</h1>            
            <p style=""color: #555;"">We're thrilled to welcome you to OmniePDV – where shopping becomes an unforgettable experience! 🎉</p>
        ";
}
