using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace github_activity.Models
{
    public class Activity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("actor")]
        public Actor actor { get; set; }

        [JsonPropertyName("repo")]
        public Repo repo { get; set; }

        [JsonPropertyName("payload")]
        public Payload payload { get; set; }

        [JsonPropertyName("public")]
        public bool Public { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
    public class Actor
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("display_login")]
        public string DisplayLogin { get; set; }
        [JsonPropertyName("gravatar_id")]
        public string GravatarId { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; }
    }
    public class Repo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
    public class Payload
    {
        [JsonPropertyName ("action")]
        public string Action { get; set; }
    }
}
