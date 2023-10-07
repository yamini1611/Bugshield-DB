using System;
using System.Collections.Generic;

namespace Bugshield.Models;

public partial class User
{
    public int Userid { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public int? Computerid { get; set; }

    public int? Roleid { get; set; }

    public virtual ComputerInfo? Computer { get; set; }

    public virtual ICollection<ErrorLog> ErrorLogs { get; set; } = new List<ErrorLog>();

    public virtual ICollection<Query> Queries { get; set; } = new List<Query>();

    public virtual Role? Role { get; set; }
}
