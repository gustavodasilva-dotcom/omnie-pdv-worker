namespace OmniePDV.ReceiptSender.Core.Models.Resources;

public sealed class MailPerson(string name, string email)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
}

public sealed class MailBody
{
    public Dictionary<string, string> Parameters { get; set; } = [];
    public string HtmlBody { get; set; } = string.Empty;
}

public sealed class MailModel
{
    public List<MailPerson> Froms { get; set; } = [];
    public List<MailPerson> Tos { get; set; } = [];
    public List<MailPerson> Ccs { get; set; } = [];
    public List<MailPerson> Bccs { get; set; } = [];
    public string Subject { get; set; } = string.Empty;
    public MailBody Body { get; set; } = new();
}
