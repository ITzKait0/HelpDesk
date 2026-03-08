namespace API_Gateway.Mail
{
    public class Mail
    {
        public long Id { get; set; }
        public long TicketId {  get; set; }
        public string From {  get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime SendDate { get; set; }
    }
}
