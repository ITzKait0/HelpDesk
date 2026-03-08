using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

[Keyless]
public partial class VwLastTicketNr
{
    [Column("TicketNR")]
    public long TicketNr { get; set; }
}
