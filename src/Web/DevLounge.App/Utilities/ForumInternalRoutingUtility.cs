namespace DevLounge.Web.Utilities
{
    /// <summary>
    /// This Utility is used to configure an internal routing functionality of hyperlinks 
    /// that lead to different layers of the content in the forum.
    /// For example, Section is layer 1, Category is layer 2, Thread is layer 3.
    /// If we enter a Thread's Details page -> We will see references to its respective Category and Section.
    /// </summary>
    public class ForumInternalRoutingUtility : IForumInternalRoutingUtility
    {
        public bool InternalRoutingVisibility { get; set; }

        public Dictionary<string, string> RoutingElements { get; set; } = new Dictionary<string, string>();
    }
}
