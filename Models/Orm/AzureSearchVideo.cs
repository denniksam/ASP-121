namespace ASP121.Models.Orm.AzureSearchVideo
{
    public class Creator
    {
        public string name { get; set; }
    }

    public class Instrumentation
    {
        public string _type { get; set; }
    }

    public class Publisher
    {
        public string name { get; set; }
    }

    public class QueryContext
    {
        public string originalQuery { get; set; }
        public string alterationDisplayQuery { get; set; }
        public string alterationOverrideQuery { get; set; }
        public string alterationMethod { get; set; }
        public string alterationType { get; set; }
    }

    public class AzureSearchVideoResponse
    {
        public string _type { get; set; }
        public Instrumentation instrumentation { get; set; }
        public string readLink { get; set; }
        public string webSearchUrl { get; set; }
        public QueryContext queryContext { get; set; }
        public int totalEstimatedMatches { get; set; }
        public List<Value> value { get; set; }
        public int nextOffset { get; set; }
        public int currentOffset { get; set; }
    }

    public class Thumbnail
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Value
    {
        public string webSearchUrl { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string thumbnailUrl { get; set; }
        public DateTime datePublished { get; set; }
        public List<Publisher> publisher { get; set; }
        public Creator creator { get; set; }
        public bool isAccessibleForFree { get; set; }
        public bool isFamilyFriendly { get; set; }
        public string contentUrl { get; set; }
        public string hostPageUrl { get; set; }
        public string encodingFormat { get; set; }
        public string hostPageDisplayUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string duration { get; set; }
        public string motionThumbnailUrl { get; set; }
        public string embedHtml { get; set; }
        public bool allowHttpsEmbed { get; set; }
        public int viewCount { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string videoId { get; set; }
        public bool allowMobileEmbed { get; set; }
        public bool isSuperfresh { get; set; }
        public string musicLyrics { get; set; }
    }
}
