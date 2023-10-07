namespace Bugshield.Models
{
    public class QueryPosted
    {
        public required string QueryDetails { get; set; }
        public DateTime? RaisedTime { get; set; }
        public DateTime? SolvedTime { get; set; }
        public int UserId { get; set; }
        public bool? IsSolved { get; set; }
    }
}
