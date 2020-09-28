using System.Collections.Generic;

namespace Twitter.Hashtag.Search.Entities
{
    public class TwitterMessage
    {
        public class Status
        {
            public string created_at { get; set; }
            public long id { get; set; }
            public string id_str { get; set; }
            public string text { get; set; }
            public Entities entities { get; set; }
            public ExtendedEntities extended_entities { get; set; }
            public User user { get; set; }
        }

        public class ExtendedEntities
        {
            public List<object> media { get; set; }
        }

        public class Entities
        {
            public List<object> hashtags { get; set; }
            public List<object> urls { get; set; }
            public List<object> media { get; set; }
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