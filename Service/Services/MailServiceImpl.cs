using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Service.Models;
using Service.Protos;

namespace Service.Services
{
    public class MailServiceImpl : MailService.MailServiceBase
    {
        private readonly HelpDeskContext _context;

        public MailServiceImpl(HelpDeskContext context)
        {
            _context = context;
        }

        public override async Task<pBoolReply> addMail(pAddMailRequest request, ServerCallContext context)
        {
            var mail = new Mail
            {
                TicketId = Convert.ToInt64(request.Subject.Split(";")[1]),
                Subject = request.Subject.Split(";")[0],
                From = request.From,
                Text = request.Text,
                SendDate = DateTime.Parse(request.SendDate)
            };
            _context.Mail.Add(mail);
            await _context.SaveChangesAsync();
            return new pBoolReply { Result = true };
        }

        public override async Task<pGetMailsForTicketIdReply> getMailsForTicketId(pGetMailsForTicketIdRequest request, ServerCallContext context)
        {
            var mails = await _context.Mail
                .Where(m => m.TicketId == request.TicketId)
                .Select(m => new pMail
                {
                    Id = m.Id,
                    TicketId = m.TicketId,
                    Subject = m.Subject,
                    From = m.From,
                    Text = m.Text,
                    SendDate = m.SendDate.ToString()
                })
                .ToListAsync();

            var reply = new pGetMailsForTicketIdReply();
            reply.Mails.AddRange(mails);
            return reply;
        }
    }
}
