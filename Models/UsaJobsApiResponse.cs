using System.Collections.Generic;

namespace careerPortal.Models
{
    public class UsaJobsApiResponse
    {
        public SearchResult SearchResult { get; set; }
    }

    public class SearchResult
    {
        public List<SearchResultItem> SearchResultItems { get; set; }
    }

    public class SearchResultItem
    {
        public MatchedObjectDescriptor MatchedObjectDescriptor { get; set; }
    }

    public class MatchedObjectDescriptor
    {
        public string PositionID { get; set; }
        public string PositionTitle { get; set; }
        public string PositionURI { get; set; }
        public string PositionLocationDisplay { get; set; }
        public string OrganizationName { get; set; }
        public List<PositionRemuneration> PositionRemuneration { get; set; }
    }

    public class PositionRemuneration
    {
        public string MinimumRange { get; set; }
        public string MaximumRange { get; set; }
        public string Description { get; set; }
    }
}