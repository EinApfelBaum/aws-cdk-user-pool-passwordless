using System.Collections.Generic;
using System.Text.Json.Serialization;
using Amazon.Lambda.Core;
using Functions.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace VerifyAuthChallenge
{
    public class VerifyAuthChallengeArguments : UserPoolBaseArguments<VerifyAuthChallengeRequest, VerifyAuthChallengeResponse>
    {
        [JsonIgnore]
        public bool? IsUserAnswerCorrect => Request.PrivateChallengeParameters["code"] == Request.ChallengeAnswer;
    }

    public class VerifyAuthChallengeResponse : UserPoolBaseResponse
    {
        [JsonPropertyName("answerCorrect")]
        public bool? AnswerCorrect { get; set; }
    }

    public class VerifyAuthChallengeRequest : UserPoolBaseRequest
    {
        [JsonPropertyName("privateChallengeParameters")]
        public Dictionary<string, string> PrivateChallengeParameters { get; set; }

        [JsonPropertyName("challengeAnswer")]
        public string ChallengeAnswer { get; set; }

        [JsonPropertyName("clientMetadata")]
        public Dictionary<string, string> ClientMetadata { get; set; }

        [JsonPropertyName("userNotFound")]
        public bool UserNotFound { get; set; }
    }

    public class Function
    {
        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="args"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public VerifyAuthChallengeArguments FunctionHandler(VerifyAuthChallengeArguments args, ILambdaContext context)
        {
            context.Logger.LogLine("start VerifyAuthChallengeArguments");

            if (args?.Request == null)
            {
                return args;
            }

            args.Response.AnswerCorrect = args.IsUserAnswerCorrect;

            return args;
        }
    }
}
