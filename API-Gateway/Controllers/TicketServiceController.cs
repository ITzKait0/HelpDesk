using Microsoft.AspNetCore.Mvc;

namespace API_Gateway.Controllers
{
    [ApiController]
    [Route("Ticket")]
    public class TicketServiceController : Controller
    {
        private readonly TicketVerwaltungsService.TicketVerwaltungsServiceClient _ticketVerwaltungsServiceClient;

        public TicketServiceController(TicketVerwaltungsService.TicketVerwaltungsServiceClient ticketVerwaltungsServiceClient)
        {
            _ticketVerwaltungsServiceClient=ticketVerwaltungsServiceClient;
        }

        [HttpPost]
        [Route("add")]
        public async Task<bool> addTicket([FromBody] TicketDto ticketDto)
        {

            pAddTicketRequest request = new pAddTicketRequest()
            {
                Ticket = new pTicket()
                {
                    KundenId = 1,
                    Prioritaet = 0,//(pPriority)Convert.ToInt32((await GeminiApiClient.Do(text))[0]),
                    Text = ticketDto.text,
                    Topic = ticketDto.topic
                }
            };

            pBoolReply reply = await _ticketVerwaltungsServiceClient.AddTicketAsync(request);
            return reply.Result;
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
