namespace DevLounge.Web.Utilities
{
    public class ForumInternalRoutingUtility : IForumInternalRoutingUtility
    {
        public bool InternalRoutingVisibility { get; set; }
        public Dictionary<string, string> RoutingElements { get; set; } = new Dictionary<string, string>();
    }
}
