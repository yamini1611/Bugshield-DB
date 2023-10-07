using System;
using System.Collections.Generic;

namespace Bugshield.Models;

public partial class ErrorLog
{
    public int ErrorLogId { get; set; }

    public int? UserId { get; set; }

    public string? Query { get; set; }

    public string? Solvedby { get; set; }

    public virtual User? User { get; set; }
}
