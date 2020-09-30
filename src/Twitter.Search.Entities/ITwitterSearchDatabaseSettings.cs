namespace Twitter.Hashtag.Search.Entities
{
    public interface ITwitterSearchDatabaseSettings
    {
        string TwitterSearchCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}