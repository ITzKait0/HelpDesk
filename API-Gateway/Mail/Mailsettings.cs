namespace API_Gateway.Mail
{
    public class MailSettings
    {
        public string ImapHost { get; set; } = "";
        public int ImapPort { get; set; }
        public string SmtpHost { get; set; } = "";
        public int SmtpPort { get; set; }
        public string GmailUser { get; set; } = "";
        public string GmailPasswort { get; set; } = "";
    }
}
