using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Twitter.Hashtag.Search.Entities
{
    public class TwitterMessage
    {
        public class Status
        {
            [JsonIgnore]
            [BsonId]
            public ObjectId mongoId { get; set; }
            public string created_at { get; set; }
            public string id_str { get; set; }
            public string text { get; set; }
            public Entities entities { get; set; }
            public ExtendedEntities extended_entities { get; set; }
            public User user { get; set; }
        }

        public class ExtendedEntities
        {
            public List<string> media { get; set; }
        }

        public class Hashtag
        {
            public string text { get; set; }
            public List<string> indices { get; set; } 
        }
        
        public class Url    {
            public string url { get; set; } 
            public string expanded_url { get; set; } 
            public string display_url { get; set; } 
            public List<string> indices { get; set; } 
        }

        public class Entities
        {
            public List<Hashtag> hashtags { get; set; }
            public List<Url> urls { get; set; }
            public List<Url> media { get; set; }
        }

        public class User
        {
            public string name { get; set; }
            public string screen_name { get; set; }
            public string profile_image_url { get; set; }
            public string profile_image_url_https { get; set; }
        }

        public List<Status> statuses { get; set; }
    }
}