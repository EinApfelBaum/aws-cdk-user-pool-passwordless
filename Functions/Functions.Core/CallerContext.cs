using System.Text.Json.Serialization;

namespace Functions.Core
{
    public class CallerContext
    {
        [JsonPropertyName("awsSdkVersion")]
        public string AwsSdkVersion { get; set; }

        [JsonPropertyName("clientId")]
        public string ClientId { get; set; }

    }
}