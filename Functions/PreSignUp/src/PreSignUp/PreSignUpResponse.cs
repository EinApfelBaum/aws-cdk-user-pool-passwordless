using System.Text.Json.Serialization;
using Functions.Core;

namespace PreSignUp
{
    public class PreSignUpResponse : UserPoolBaseResponse
    {
        [JsonPropertyName("autoConfirmUser")]
        public bool? AutoConfirmUser { get; set; }

        [JsonPropertyName("autoVerifyEmail")]
        public bool? AutoVerifyEmail { get; set; }

        [JsonPropertyName("autoVerifyPhone")]
        public bool? AutoVerifyPhone { get; set; }
    }
}