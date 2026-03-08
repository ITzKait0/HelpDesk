using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

[Table("Ticket")]
public partial class Ticket
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("TicketNR")]
    public long TicketNr { get; set; }

    [StringLength(50)]
    public string Topic { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Firstname { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    public int Priority { get; set; }

    public string Text { get; set; } = null!;

    [Column("Supporter_ID")]
    public int? SupporterId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Created { get; set; }

    [InverseProperty("Ticket")]
    public virtual ICollection<Mail> Mail { get; set; } = new List<Mail>();

    [ForeignKey("SupporterId")]
    [InverseProperty("Tickets")]
    public virtual Supporter? Supporter { get; set; }
}
