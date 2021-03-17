using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PreSignUp
{
    public class Function
    {
        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="args"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public PreSignUpArguments FunctionHandler(PreSignUpArguments args, ILambdaContext context)
        {
            args.Response.AutoConfirmUser = true;
            args.Response.AutoVerifyPhone = true;
            args.Response.AutoVerifyEmail = true;
            return args;
        }
    }
}
