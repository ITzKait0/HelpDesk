using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Gateway.Controllers
{
    [ApiController]
    [Route("Kunde")]
    public class KundenServiceController : ControllerBase
    {
        private readonly KundenVerwaltungsService.KundenVerwaltungsServiceClient _kundenVerwaltungsClient;

        public KundenServiceController(KundenVerwaltungsService.KundenVerwaltungsServiceClient kundenVerwaltungsClient)
        {
            _kundenVerwaltungsClient=kundenVerwaltungsClient;
        }

        [HttpPost]
        [Route("add")]
        public async Task<bool> AddUser([FromBody] Kunde kunde)
        {
            pBoolReply reply = await _kundenVerwaltungsClient.AddKundeAsync(new pAddKundeRequest()
            {
                Kunde = new pKunde()
                {
                    Name = kunde.Name,
                    Vorname = kunde.Vorname,
                    Adresse = kunde.Adresse,
                    Plz = kunde.Plz,
                    Tel = kunde.Tel
                }
            });
            return reply.Result;
        }

        public class Kunde
        {

            public string Name { get; set; } = null!;

            public string Vorname { get; set; } = null!;

            public string Tel { get; set; } = null!;

            public string Adresse { get; set; } = null!;

            public int Plz { get; set; }

        }
    }
}
