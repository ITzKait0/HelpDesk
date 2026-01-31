using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Supporter
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Vorname { get; set; } = null!;

    public string Durchwahl { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
