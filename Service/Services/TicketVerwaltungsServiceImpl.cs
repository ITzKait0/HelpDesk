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

        public override async Task<pAddTicketReply> AddTicket(pAddTicketRequest request, ServerCallContext context)
        {
            pTicket ticket = request.Ticket;

            Ticket ticketEntity = new Ticket()
            {
                TicketNr = 0,
                Name = ticket.Name,
                Firstname = ticket.Firstname,
                Email = ticket.Email,
                Priority = (int)ticket.Priority,
                SupporterId = 1,
                Topic = ticket.Topic,
                Text = ticket.Text,
                Created = DateTime.Now
            };
            await _context.Tickets.AddAsync(ticketEntity);
            await _context.SaveChangesAsync();

            return new pAddTicketReply() { Id = ticketEntity.Id };
        }

        public override async Task<pGetTicketsBySupporterIdReply> GetTicketsBySupporterId(pGetTicketsBySupporterIdRequest request, ServerCallContext context)
        {
            if (request.SupporterId != 0)
            {
                return new pGetTicketsBySupporterIdReply()
                {
                    Tickets = { await _context.Tickets.Where(t => t.SupporterId == request.SupporterId).Select(t => new pTicket()
                    {
                        Id = t.Id,
                        Ticketnr = t.TicketNr,
                        Name = t.Name,
                        Firstname = t.Firstname,
                        Email = t.Email,
                        Priority = (pPriority)t.Priority,
                        Supporterid = t.SupporterId.Value,
                        Topic = t.Topic,
                        Text = t.Text,
                        Created = t.Created.ToString()
                    }).ToListAsync() }
                };
            }
            throw new RpcException(new Status(StatusCode.InvalidArgument, "SupporterId must be provided and not 0"));
        }

        public override async Task<pGetTicketByIdReply> GetTicketById(pGetTicketByIdRequest request, ServerCallContext context)
        {
            if (request.Id != 0)
            {
                return new pGetTicketByIdReply()
                {
                    Ticket = await _context.Tickets.Where(t => t.Id == request.Id).Select(t => new pTicket()
                    {
                        Id = t.Id,
                        Ticketnr = t.TicketNr,
                        Name = t.Name,
                        Firstname = t.Firstname,
                        Email = t.Email,
                        Priority = (pPriority)t.Priority,
                        Supporterid = t.SupporterId.Value,
                        Topic = t.Topic,
                        Text = t.Text,
                        Created = t.Created.ToString()
                    }).FirstOrDefaultAsync()
                };
            }
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Id must be provided and not 0"));
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
