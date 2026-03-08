using API_Gateway.Mail;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API_Gateway.Controllers
{
    [ApiController]
    [Route("Ticket")]
    public class TicketServiceController : Controller
    {
        private readonly TicketVerwaltungsService.TicketVerwaltungsServiceClient _ticketVerwaltungsServiceClient;
        private readonly GmailConnection _gmailConnection;

        public TicketServiceController(TicketVerwaltungsService.TicketVerwaltungsServiceClient ticketVerwaltungsServiceClient, GmailConnection gmailConnection)
        {
            _ticketVerwaltungsServiceClient=ticketVerwaltungsServiceClient;
            _gmailConnection=gmailConnection;
        }

        [HttpPost]
        [Route("add")]
        public async Task<bool> addTicket([FromBody] TicketDto ticketDto)
        {
            Tuple<int, string> geminiApiEvaluationRequest = await GeminiApiClient.Do(ticketDto.text);
            Console.WriteLine("GEMINI CONTROLLER RESULT:" + geminiApiEvaluationRequest.Item1);
            pAddTicketRequest request = new pAddTicketRequest() {
                Ticket = new pTicket() {
                    Id = 0,
                    Ticketnr = 0,
                    Name = ticketDto.name,
                    Firstname = ticketDto.firstname,
                    Email = ticketDto.email,
                    Priority = (pPriority)geminiApiEvaluationRequest.Item1,//(pPriority)Convert.ToInt32((await GeminiApiClient.Do(text))[0]),
                    Text = ticketDto.text,
                    Topic = ticketDto.topic,
                    Supporterid = 0,
                }
            };

            pAddTicketReply reply = await _ticketVerwaltungsServiceClient.AddTicketAsync(request);
            await _gmailConnection.SendAsync(ticketDto.email, $"Ticket: {ticketDto.topic};{reply.Id}", $"Guten Tag, \n ihre TicketNr: {reply.Id} \n bitte bei weiterem Email Verkehr immer auf diese Emails antworten und den Betreff nicht ändern. \n Mit freundlichen Grüßen \n Ihre F.H.P Enterprise Estates");
            return true;
        }

        [HttpGet]
        [Route("byId/{id}")]
        public async Task<pTicket> getTicketById(long id)
        {
            pGetTicketByIdRequest request = new pGetTicketByIdRequest() { Id = id };
            pGetTicketByIdReply reply = await _ticketVerwaltungsServiceClient.GetTicketByIdAsync(request);
            return reply.Ticket;
        }

        [HttpGet]
        [Route("bySupporterId/{supporterId}")]
        public async Task<List<pTicket>> getTicketsBySupporterId(int supporterId)
        {
            pGetTicketsBySupporterIdRequest request = new pGetTicketsBySupporterIdRequest() { SupporterId = supporterId };
            pGetTicketsBySupporterIdReply reply = await _ticketVerwaltungsServiceClient.GetTicketsBySupporterIdAsync(request);
            return reply.Tickets.ToList();
        }
        
        public class TicketDto
        {
            public string name { get; set; }
            public string firstname { get; set; }
            public string email { get; set; }
            public string topic { get; set; }
            public string text { get; set; }
        }

        public enum Priority
        {
            NON_VALUE = 0,
            SEHR_NIEDRIG = 1,
            NIEDRIG = 2,
            MITTEL = 3,
            HOCH = 4,
            SEHR_HOCH = 5
        }
    }
}
