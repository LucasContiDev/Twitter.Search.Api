using System.Threading.Tasks;
using Refit;
using Twitter.Hashtag.Search.Entities;

namespace Twitter.Search.Services.Abstraction
{
    public interface ITwitterMessageApiService
    {
        [Get("/1.1/search/tweets.json?q=%23{hashtagParameter}&result_type=recent&expansions=attachments.media_keys&media.fields=duration_ms,height,media_key,preview_image_url,public_metrics,type,url,width")]
        [Headers("Authorization: Bearer")]
        Task<TwitterApiResponse> GetRecentTweetsByHashtag(string hashtagParameter);
    }
}