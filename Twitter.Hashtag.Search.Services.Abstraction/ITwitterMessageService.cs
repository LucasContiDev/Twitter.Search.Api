using System.Threading.Tasks;
using Twitter.Hashtag.Search.Entities;

namespace Twitter.Hashtag.Search.Services.Abstraction
{
    public interface ITwitterMessageService
    {
       Task<TwitterMessage> GetMessagesByHashtag(string hashtag);
    }
}