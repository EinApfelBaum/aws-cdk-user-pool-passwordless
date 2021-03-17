using System.Text.Json.Serialization;
using Functions.Core;

namespace DefineAuthChallenge
{
    public class DefinedAuthChallengeResponse : UserPoolBaseResponse
    {
        [JsonPropertyName("challengeName")]
        public string ChallengeName { get; set; }

        /// <summary>
        /// Set to true if you determine that the user has been sufficiently authenticated by completing the challenges, or false otherwise.
        /// </summary>
        [JsonPropertyName("issueTokens")]
        public bool? IssueTokens { get; set; }

        /// <summary>
        /// Set to true if you want to terminate the current authentication process, or false otherwise.
        /// </summary>
        [JsonPropertyName("failAuthentication")]
        public bool? FailAuthentication { get; set; }
    }
}