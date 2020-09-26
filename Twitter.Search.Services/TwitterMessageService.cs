using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using Twitter.Hashtag.Search.Entities;
using Twitter.Search.Services.Abstraction;

namespace Twitter.Search.Services
{
    public class TwitterMessageService : ITwitterMessageService
    {
        private readonly ITwitterMessageApiService _twitterMessageApiService;

        private const string HostUrl = "https://api.twitter.com";

        private const string TwitterToken =
            "AAAAAAAAAAAAAAAAAAAAAH4uIAEAAAAA8Z3pHkd9lId2kK%2FRW4af%2Fe5J%2FM4%3D5lEr4loM0LjpDwSwgAAteIjxa8tO39lkULVWazFMWAdoK6vh1Y";

        public TwitterMessageService()
        {
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
            var twitterMessage = ConvertToSimpleTwitterMessage(twitterApiResponse);
            return twitterMessage;
        }

        private static TwitterMessage ConvertToSimpleTwitterMessage(TwitterApiResponse twitterApiResponse)
        {
            var jsonTwitterApiResponse = JsonConvert.SerializeObject(twitterApiResponse);
            var twitterMessage = JsonConvert.DeserializeObject<TwitterMessage>(jsonTwitterApiResponse);
            return twitterMessage;
        }
    }
}