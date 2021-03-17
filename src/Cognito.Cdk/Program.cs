using Amazon.CDK;

namespace Cognito.Cdk
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new CognitoStack(app, "test-cognito", new MyStackProps { Stage = "test" });
            app.Synth();
        }
    }
}
