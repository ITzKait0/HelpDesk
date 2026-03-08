using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

public partial class Mail
{
    [Key]
    public long Id { get; set; }

    public long TicketId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Subject { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string From { get; set; } = null!;

    [StringLength(1000)]
    [Unicode(false)]
    public string Text { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime SendDate { get; set; }

    [ForeignKey("TicketId")]
    [InverseProperty("Mail")]
    public virtual Ticket Ticket { get; set; } = null!;
}
