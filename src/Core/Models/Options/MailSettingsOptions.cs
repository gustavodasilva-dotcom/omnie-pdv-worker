namespace OmniePDV.ReceiptSender.Core.Models.Options;

public sealed class MailNoReplySender
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public sealed class MailSMTP
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public sealed class MailSettingsOptions
{
    public const string Position = "MailSettings";

    public MailNoReplySender NoReplySender { get; set; }
    public MailSMTP SMTP { get; set; }
}
