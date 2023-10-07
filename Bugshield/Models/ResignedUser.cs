using System;
using System.Collections.Generic;

namespace Bugshield.Models;

public partial class ResignedUser
{
    public int Resno { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? Roleid { get; set; }
}
