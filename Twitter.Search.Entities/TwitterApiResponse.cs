using System;
using System.Collections.Generic;

namespace Twitter.Hashtag.Search.Entities
{
    public class TwitterApiResponse
    {
    public class Url    {
        public string url { get; set; } 
        public string expanded_url { get; set; } 
        public string display_url { get; set; } 
        public List<string> indices { get; set; } 
    }

    public class Entities    {
        public List<object> hashtags { get; set; } 
        public List<object> symbols { get; set; } 
        public List<object> user_mentions { get; set; } 
        public List<Url> urls { get; set; } 
    }

    public class Metadata    {
        public string result_type { get; set; } 
        public string iso_language_code { get; set; } 
    }

    public class Url3    {
        public string url { get; set; } 
        public string expanded_url { get; set; } 
        public string display_url { get; set; } 
        public List<string> indices { get; set; } 
    }

    public class Url2    {
        public List<Url3> urls { get; set; } 
    }

    public class Description    {
        public List<object> urls { get; set; } 
    }

    public class Entities2    {
        public Url2 url { get; set; } 
        public Description description { get; set; } 
    }

    public class User    {
        public string id { get; set; } 
        public string id_str { get; set; } 
        public string name { get; set; } 
        public string screen_name { get; set; } 
        public string location { get; set; } 
        public string description { get; set; } 
        public string url { get; set; } 
        public Entities2 entities { get; set; } 
        public bool @protected { get; set; } 
        public string followers_count { get; set; } 
        public string friends_count { get; set; } 
        public string listed_count { get; set; } 
        public string created_at { get; set; } 
        public string favourites_count { get; set; } 
        public string utc_offset { get; set; } 
        public string time_zone { get; set; } 
        public bool geo_enabled { get; set; } 
        public bool verified { get; set; } 
        public string statuses_count { get; set; } 
        public string lang { get; set; } 
        public bool contributors_enabled { get; set; } 
        public bool is_translator { get; set; } 
        public bool is_translation_enabled { get; set; } 
        public string profile_background_color { get; set; } 
        public string profile_background_image_url { get; set; } 
        public string profile_background_image_url_https { get; set; } 
        public bool profile_background_tile { get; set; } 
        public string profile_image_url { get; set; } 
        public string profile_image_url_https { get; set; } 
        public string profile_banner_url { get; set; } 
        public string profile_link_color { get; set; } 
        public string profile_sidebar_border_color { get; set; } 
        public string profile_sidebar_fill_color { get; set; } 
        public string profile_text_color { get; set; } 
        public bool profile_use_background_image { get; set; } 
        public bool has_extended_profile { get; set; } 
        public bool default_profile { get; set; } 
        public bool default_profile_image { get; set; } 
        public object following { get; set; } 
        public object follow_request_sent { get; set; } 
        public object notifications { get; set; } 
        public string translator_type { get; set; } 
    }

    public class Status    {
        public string created_at { get; set; } 
        public long id { get; set; } 
        public string id_str { get; set; } 
        public string text { get; set; } 
        public bool truncated { get; set; } 
        public Entities entities { get; set; } 
        public Metadata metadata { get; set; } 
        public string source { get; set; } 
        public object in_reply_to_status_id { get; set; } 
        public object in_reply_to_status_id_str { get; set; } 
        public object in_reply_to_user_id { get; set; } 
        public object in_reply_to_user_id_str { get; set; } 
        public object in_reply_to_screen_name { get; set; } 
        public User user { get; set; } 
        public object geo { get; set; } 
        public object coordinates { get; set; } 
        public object place { get; set; } 
        public object contributors { get; set; } 
        public bool is_quote_status { get; set; } 
        public string retweet_count { get; set; } 
        public string favorite_count { get; set; } 
        public bool favorited { get; set; } 
        public bool retweeted { get; set; } 
        public bool possibly_sensitive { get; set; } 
        public string lang { get; set; } 
    }
    
    public List<Status> statuses { get; set; } 
    }
}