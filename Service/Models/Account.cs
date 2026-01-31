using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Account
{
    public long Id { get; set; }

    public int SupporterId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Supporter Supporter { get; set; } = null!;
}
