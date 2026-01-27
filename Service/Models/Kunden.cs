using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

[Table("Kunden")]
public partial class Kunden
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Vorname { get; set; } = null!;

    [StringLength(30)]
    public string Tel { get; set; } = null!;

    [StringLength(200)]
    public string Adresse { get; set; } = null!;

    [Column("PLZ")]
    public int Plz { get; set; }

    [InverseProperty("Kunden")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("Kunden")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
