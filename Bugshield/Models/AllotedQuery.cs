using System;
using System.Collections.Generic;

namespace Bugshield.Models;

public partial class AllotedQuery
{
    public int Alotid { get; set; }

    public string? AllotedQueries { get; set; }

    public DateTime? RaisedTime { get; set; }

    public DateTime? SolvedTime { get; set; }

    public int? RaisedUser { get; set; }

    public string? Sauser { get; set; }

    public string? Progress { get; set; }

    public string? Remarks { get; set; }

    public string? Status { get; set; }
}
