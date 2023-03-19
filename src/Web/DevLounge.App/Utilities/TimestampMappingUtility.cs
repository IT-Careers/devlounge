namespace DevLounge.Web.Utilities
{
    public class TimestampMappingUtility : ITimestampMappingUtility
    {
        public string MapTimestamp(DateTime timestamp) => DateTime.Now.Subtract(timestamp) switch {
            { TotalDays: > 730 } => "on " + timestamp.ToString("dd/MM/yyyy"),
            { TotalDays: > 365 } and { TotalDays: <= 730 } => "1 year and " + (((DateTime.Now.Month - timestamp.Month) + 12 * (DateTime.Now.Year - timestamp.Year)) - 12),
            { TotalDays: > 30 } and { TotalDays: <= 365 } => (DateTime.Now.Month - timestamp.Month) + 12 * (DateTime.Now.Year - timestamp.Year) + " months ago",
            { TotalDays: > 7 } and { TotalDays: <= 30 } => ((int) (DateTime.Now - timestamp).TotalDays / 7) + " weeks ago",
            { TotalDays: >= 1 } and { TotalDays: <= 7 } => ((int)(DateTime.Now - timestamp).TotalDays) + " days ago",
            { TotalDays: < 1 } and { TotalMinutes: > 60 } => ((int)(DateTime.Now - timestamp).TotalHours) + " hours ago",
            { TotalMinutes: >= 1 } and { TotalMinutes: <= 60 } => ((int)(DateTime.Now - timestamp).TotalMinutes) + " minutes ago",
            { TotalSeconds: >= 0 } and { TotalMinutes: < 1 } => ((int)(DateTime.Now - timestamp).TotalSeconds) + " seconds ago",
            _ => throw new InvalidOperationException("Invalid timestamp provided...")
        };
    }
}
