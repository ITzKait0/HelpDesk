using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

[Index("Benutzername", Name = "UQ_Account_Benutzername", IsUnique = true)]
public partial class Account
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("Kunden_ID")]
    public int? KundenId { get; set; }

    [Column("Supporter_ID")]
    public int? SupporterId { get; set; }

    [StringLength(100)]
    public string Benutzername { get; set; } = null!;

    [StringLength(200)]
    public string Passwort { get; set; } = null!;

    [ForeignKey("KundenId")]
    [InverseProperty("Accounts")]
    public virtual Kunden? Kunden { get; set; }

    [ForeignKey("SupporterId")]
    [InverseProperty("Accounts")]
    public virtual Supporter? Supporter { get; set; }
}
