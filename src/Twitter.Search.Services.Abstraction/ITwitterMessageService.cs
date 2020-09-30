using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter.Hashtag.Search.Entities;

namespace Twitter.Search.Services.Abstraction
{
    public interface ITwitterMessageService
    {
       Task<TwitterMessage> GetMessagesByHashtag(string hashtag);
       List<TwitterMessage.Status> RetrieveMessagesFromDatabase();
    }
}