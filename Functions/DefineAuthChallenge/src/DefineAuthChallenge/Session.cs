using System.Text.Json.Serialization;

namespace DefineAuthChallenge
{
    public class Session
    {
        [JsonPropertyName("challengeName")]
        public string ChallengeName { get; set; }

        [JsonPropertyName("challengeResult")]
        public bool ChallengeResult { get; set; }

        [JsonPropertyName("challengeMetadata")]
        public string ChallengeMetadata { get; set; }
    }
}