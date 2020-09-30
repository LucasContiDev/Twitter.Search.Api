namespace Twitter.Hashtag.Search.Entities
{
    public class TwitterSearchDatabaseSettings : ITwitterSearchDatabaseSettings
    {
        public string TwitterSearchCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}