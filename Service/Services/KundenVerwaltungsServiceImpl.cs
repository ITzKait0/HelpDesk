using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Models;
using Service.Protos;
namespace Service.Services
{
    public class KundenVerwaltungsServiceImpl: KundenVerwaltungsService.KundenVerwaltungsServiceBase 
    {
        private readonly HelpDeskContext _context;

        public KundenVerwaltungsServiceImpl(HelpDeskContext context)
        {
            _context = context;
        }

        public override async Task<pBoolReply> AddKunde(pAddKundeRequest request, ServerCallContext context)
        {
            EntityEntry<Kunden> entityEntry = _context.Kundens.Add(new Kunden()
            {
                Name = request.Kunde.Name,
                Vorname = request.Kunde.Vorname,
                Tel = request.Kunde.Tel,
                Plz = request.Kunde.Plz,
                Adresse = request.Kunde.Adresse
            });

            await _context.SaveChangesAsync();

            return new pBoolReply {Result = entityEntry != null};
        }
    }
}
