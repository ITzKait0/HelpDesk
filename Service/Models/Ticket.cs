using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Ticket
{
    public long Id { get; set; }

    public long TicketNr { get; set; }

    public string Topic { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Priority { get; set; }

    public string Text { get; set; } = null!;

    public int? SupporterId { get; set; }

    public DateTime Created { get; set; }

    public virtual Supporter? Supporter { get; set; }
}
