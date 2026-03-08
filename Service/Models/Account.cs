using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

[Table("Account")]
public partial class Account
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("Supporter_ID")]
    public int SupporterId { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(50)]
    public string Password { get; set; } = null!;

    [ForeignKey("SupporterId")]
    [InverseProperty("Accounts")]
    public virtual Supporter Supporter { get; set; } = null!;
}
