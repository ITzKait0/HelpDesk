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
                TicketNr = 0,
                Name = ticket.Name,
                Firstname = ticket.Firstname,
                Email = ticket.Email,
                Priority = 0,
                SupporterId = 1,
                Topic = ticket.Topic,
                Text = ticket.Text,
                Created = DateTime.Now,
            });
            await _context.SaveChangesAsync();

            return new pBoolReply() { Result = true };
        }
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
