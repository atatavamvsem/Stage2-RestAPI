using Newtonsoft.Json;

namespace RestAPITests.DataEntities
{
    class Post
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
