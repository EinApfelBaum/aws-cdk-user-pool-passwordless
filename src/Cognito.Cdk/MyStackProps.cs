using Amazon.CDK;

namespace Cognito.Cdk
{
    public class MyStackProps : StackProps
    {
        public string Stage { get; set; }
    }
}