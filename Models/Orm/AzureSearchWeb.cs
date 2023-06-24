namespace ASP121.Models.Orm.AzureSearchWeb
{
    public class DeepLink
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Item
    {
        public string answerType { get; set; }
        public int resultIndex { get; set; }
        public Value value { get; set; }
    }

    public class Mainline
    {
        public List<Item> items { get; set; }
    }

    public class QueryContext
    {
        public string originalQuery { get; set; }
    }

    public class RankingResponse
    {
        public Mainline mainline { get; set; }
    }

    public class RelatedSearches
    {
        public string id { get; set; }
        public List<Value> value { get; set; }
    }

    public class AzureSearchWebResponse
    {
        public string _type { get; set; }
        public QueryContext queryContext { get; set; }
        public WebPages webPages { get; set; }
        public RelatedSearches relatedSearches { get; set; }
        public RankingResponse rankingResponse { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public bool isFamilyFriendly { get; set; }
        public string displayUrl { get; set; }
        public string snippet { get; set; }
        public List<DeepLink> deepLinks { get; set; }
        public DateTime dateLastCrawled { get; set; }
        public string language { get; set; }
        public bool isNavigational { get; set; }
        public string thumbnailUrl { get; set; }
        public string text { get; set; }
        public string displayText { get; set; }
        public string webSearchUrl { get; set; }
    }

    public class WebPages
    {
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
        public List<Value> value { get; set; }
    }
}
