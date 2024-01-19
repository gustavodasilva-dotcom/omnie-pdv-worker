namespace OmniePDV.ReceiptSender.Core.Models.Options;

public sealed class ApiOptions
{
    public const string Position = "Api";

    public string Uri { get; set; } = string.Empty;
    public string DefaultResourcesUri { get; set; } = string.Empty;
    public string PublicResourcesUri { get; set; } = string.Empty;
}
