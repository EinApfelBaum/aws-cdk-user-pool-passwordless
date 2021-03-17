using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Functions.Core;

namespace DefineAuthChallenge
{
    public class DefinedAuthChallengeRequest : UserPoolBaseRequest
    {
        [JsonPropertyName("userNotFound")]
        public bool? UserNotFound { get; set; }

        [JsonPropertyName("session")]
        public Session[] Session { get; set; }

        [JsonPropertyName("clientMetaData")]
        public Dictionary<string, string> ClientMetaData { get; set; }

        [JsonIgnore]
        public bool HasReachedMaxAttempts => Session.Length >= 4;

        [JsonIgnore]
        public bool IsCustomChallenge => Session.All(session => session.ChallengeName == "CUSTOM_CHALLENGE");

        [JsonIgnore]
        public bool IsChallengeRequest => Session.Length == 0;
    }
}