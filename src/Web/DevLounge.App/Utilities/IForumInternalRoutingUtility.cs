namespace DevLounge.Web.Utilities
{
    public interface IForumInternalRoutingUtility
    {
        bool InternalRoutingVisibility { get; set; }
        
        Dictionary<string, string> RoutingElements { get; set; }
    }
}
