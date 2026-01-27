using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

public partial class Ticket
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(200)]
    public string Titel { get; set; } = null!;

    public string Inhalt { get; set; } = null!;

    public int? Prioritaet { get; set; }

    [Column("Kunden_ID")]
    public int KundenId { get; set; }

    [ForeignKey("KundenId")]
    [InverseProperty("Tickets")]
    public virtual Kunden Kunden { get; set; } = null!;
}
