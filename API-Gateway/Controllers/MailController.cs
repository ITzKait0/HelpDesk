using API_Gateway.Mail;
using Microsoft.AspNetCore.Mvc;

namespace API_Gateway.Controllers
{
    [ApiController]
    [Route("Mail")]
    public class MailController : Controller
    {
        private readonly MailService.MailServiceClient _mailServiceClient;
        private readonly GmailConnection _gmailConnection;

        public MailController(MailService.MailServiceClient mailServiceClient, GmailConnection gmailConnection)
        {
            _mailServiceClient = mailServiceClient;
            _gmailConnection = gmailConnection;
        }

        [HttpGet]
        [Route("byTicketId/{ticketId}")]
        public async Task<List<pMail>> getMailsForTicketId(int ticketId)
        {
            pGetMailsForTicketIdRequest request = new pGetMailsForTicketIdRequest() { TicketId = ticketId };
            pGetMailsForTicketIdReply reply = await _mailServiceClient.getMailsForTicketIdAsync(request);
            return reply.Mails.ToList();
        }

        [HttpPost]
        [Route("add/{ticketId}")]
        public async Task<bool> sendMail(int ticketId, [FromBody] SendMailDto mailDto)
        {
            pAddMailRequest request = new pAddMailRequest()
            {
                Subject = $"{mailDto.Subject};{ticketId}",
                From = mailDto.From,
                Text = mailDto.Text,
                SendDate = DateTime.Now.ToString()
            };
            await _gmailConnection.SendAsync(mailDto.To, $"{mailDto.Subject};{ticketId}", mailDto.Text);
            pBoolReply reply = await _mailServiceClient.addMailAsync(request);
            return reply.Result;
        }

        public class SendMailDto
        {
            public string Subject { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string Text { get; set; }
        }
    }
}
