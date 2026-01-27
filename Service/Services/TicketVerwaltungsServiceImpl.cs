using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Service.Models;
using Service.Protos;

namespace Service.Services
{
    public class TicketVerwaltungsServiceImpl : TicketVerwaltungsService.TicketVerwaltungsServiceBase
    {
        private readonly HelpDeskContext _context;

        public TicketVerwaltungsServiceImpl(HelpDeskContext context)
        {
            _context=context;
        }

        public override async Task<pBoolReply> AddTicket(pAddTicketRequest request, ServerCallContext context)
        {
            pTicket ticket = request.Ticket;

            _context.Tickets.Add(new Ticket()
            {
                Titel = ticket.Topic,
                Inhalt = ticket.Text,
                KundenId = ticket.KundenId,
                Prioritaet = (int)ticket.Prioritaet
            });
            await _context.SaveChangesAsync();

            return new pBoolReply() { Result = true };
        }
    }
}
