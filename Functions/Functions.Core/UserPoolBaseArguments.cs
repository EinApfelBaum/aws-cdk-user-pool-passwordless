using System;
using System.Text.Json.Serialization;

namespace Functions.Core
{
    public abstract class UserPoolBaseArguments<TRequest, TResponse>
        where TRequest : UserPoolBaseRequest
        where TResponse : UserPoolBaseResponse
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("triggerSource")]
        public string TriggerSource { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("userPoolId")]
        public string UserPoolId { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("callerContext")]
        public CallerContext CallerContext { get; set; }

        [JsonPropertyName("request")]
        public TRequest Request { get; set; }

        [JsonPropertyName("response")]
        public TResponse Response { get; set; }
    }
}
