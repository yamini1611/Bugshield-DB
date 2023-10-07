using System;
using System.Collections.Generic;

namespace Bugshield.Models;

public partial class SolvedQuery
{
    public int SolvedQueryId { get; set; }

    public int QueryId { get; set; }

    public DateTime SolvedTime { get; set; }

    public string SolvedBy { get; set; } = null!;

    public string SolvedDetails { get; set; } = null!;

    public virtual Query Query { get; set; } = null!;
}
