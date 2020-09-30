using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using Refit;
using Twitter.Hashtag.Search.Entities;
using Twitter.Search.Services.Abstraction;

namespace Twitter.Search.Services
{
    public class TwitterMessageService : ITwitterMessageService
    {
        private readonly ILogger<TwitterMessageService> _logger;
        private readonly ITwitterMessageApiService _twitterMessageApiService;
        private readonly IMongoCollection<TwitterMessage.Status> _twitterMessageCollection;


        private const string HostUrl = "https://api.twitter.com";

        private const string TwitterToken =
            "AAAAAAAAAAAAAAAAAAAAAH4uIAEAAAAA8Z3pHkd9lId2kK%2FRW4af%2Fe5J%2FM4%3D5lEr4loM0LjpDwSwgAAteIjxa8tO39lkULVWazFMWAdoK6vh1Y";

        public TwitterMessageService(ILogger<TwitterMessageService> logger,
            ITwitterSearchDatabaseSettings twitterSearchDatabaseSettings)
        {
            var client = new MongoClient(twitterSearchDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(twitterSearchDatabaseSettings.DatabaseName);
            _twitterMessageCollection = database.GetCollection<TwitterMessage.Status>(twitterSearchDatabaseSettings.TwitterSearchCollectionName);
            
            _logger = logger;
            _twitterMessageApiService = RestService.For<ITwitterMessageApiService>(HostUrl,
                new RefitSettings()
                {
                    AuthorizationHeaderValueGetter = () =>
                        Task.FromResult(TwitterToken)
                });
        }

        public async Task<TwitterMessage> GetMessagesByHashtag(string hashtag)
        {
            var twitterApiResponse = await _twitterMessageApiService.GetRecentTweetsByHashtag(hashtag);
            _logger.LogInformation(JsonConvert.SerializeObject(twitterApiResponse));
            var twitterMessage = ConvertToSimpleTwitterMessage(twitterApiResponse);
            _twitterMessageCollection.InsertMany(twitterMessage.statuses);
            return twitterMessage;
        }
        
        public List<TwitterMessage.Status> RetrieveMessagesFromDatabase()
        {
            var twitterMessagesFromDatabase = _twitterMessageCollection.Find(message => true).ToList();
            return twitterMessagesFromDatabase;
        }

        private static TwitterMessage ConvertToSimpleTwitterMessage(TwitterApiResponse twitterApiResponse)
        {
            var jsonTwitterApiResponse = JsonConvert.SerializeObject(twitterApiResponse);
            var twitterMessage = JsonConvert.DeserializeObject<TwitterMessage>(jsonTwitterApiResponse);
            return twitterMessage;
        }
    }
}