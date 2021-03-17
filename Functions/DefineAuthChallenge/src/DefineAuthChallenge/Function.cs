using System.Linq;
using Amazon.Lambda.Core;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DefineAuthChallenge
{
    public class Function
    {
        private const string CustomChallengeName = "CUSTOM_CHALLENGE";

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="args"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public DefineAuthChallengeArguments FunctionHandler(DefineAuthChallengeArguments args, ILambdaContext context)
        {
            context.Logger.LogLine("start DefineAuthChallengeArguments");

            if (args?.Request?.Session == null)
            {
                return args;
            }

            if(args.Request.IsChallengeRequest)
            {
                context.Logger.LogLine("User requests the challenge");

                args.Response.FailAuthentication = false;
                args.Response.IssueTokens = false;
                args.Response.ChallengeName = CustomChallengeName;
            }
            else if (!args.Request.IsCustomChallenge)
            {
                context.Logger.LogLine("Custom challenge not found");
                
                args.Response.FailAuthentication = true;
                args.Response.IssueTokens = false;
            }
            else if (args.Request.HasReachedMaxAttempts)
            {
                context.Logger.LogLine("More than three attempts");
                args.Response.FailAuthentication = true;
                args.Response.IssueTokens = false;
            }
            else if (args.Request.Session.Last().ChallengeResult)
            {
                context.Logger.LogLine("User login successful, user sends the right code.");
                
                args.Response.FailAuthentication = false;
                args.Response.IssueTokens = true;
            }
            else
            {
                context.Logger.LogLine("User sends wrong code.");

                // The user did not provide a correct answer yet; present challenge
                args.Response.FailAuthentication = false;
                args.Response.IssueTokens = false;
                args.Response.ChallengeName = CustomChallengeName;
            }

            context.Logger.LogLine($"challenge: {args.Response.ChallengeName}");
            context.Logger.LogLine($"issueToken: {args.Response.IssueTokens}");
            context.Logger.LogLine($"FailAuthentication: {args.Response.FailAuthentication}");

            return args;
        }
    }
}
