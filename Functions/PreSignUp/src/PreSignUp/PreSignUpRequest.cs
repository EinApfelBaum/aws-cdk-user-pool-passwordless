using System.Collections.Generic;
using System.Text.Json.Serialization;
using Functions.Core;

namespace PreSignUp
{
    public class PreSignUpRequest : UserPoolBaseRequest
    {
        [JsonPropertyName("validationData")]
        public Dictionary<string, string> ValidationData { get; set; }
    }
}