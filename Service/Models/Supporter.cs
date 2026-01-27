using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

[Table("Supporter")]
public partial class Supporter
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Vorname { get; set; } = null!;

    [StringLength(30)]
    public string Durchwahl { get; set; } = null!;

    [StringLength(150)]
    public string Email { get; set; } = null!;

    [InverseProperty("Supporter")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
