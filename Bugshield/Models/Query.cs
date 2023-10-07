using System;
using System.Collections.Generic;

namespace Bugshield.Models;

public partial class Query
{
    public int QueryId { get; set; }

    public DateTime? RaisedTime { get; set; }

    public DateTime? SolvedTime { get; set; }

    public string QueryDetails { get; set; } = null!;

    public int UserId { get; set; }

    public bool? IsSolved { get; set; }

    public virtual User User { get; set; } = null!;
}
