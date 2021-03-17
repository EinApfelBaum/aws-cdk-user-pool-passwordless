using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Functions.Core
{
    public abstract class UserPoolBaseRequest
    {
        [JsonPropertyName("userAttributes")]
        public Dictionary<string, string> UserAttributes { get; set; }
    }
}