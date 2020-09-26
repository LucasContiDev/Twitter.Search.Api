using System.Threading.Tasks;
using Refit;
using Twitter.Hashtag.Search.Entities;

namespace Twitter.Hashtag.Search.Services.Abstraction
{
    public interface ITwitterMessageApiService
    {
        [Get("/1.1/search/tweets.json?q=%23{hashtagParameter}&result_type=recent")]
        [Headers("Authorization: Bearer")]
        Task<TwitterApiResponse> GetRecentTweetsByHashtag(string hashtagParameter);
    }
}