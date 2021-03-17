using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;


namespace Cognito.Cdk
{
    public class CognitoStack : Stack
    {
        internal CognitoStack(Construct scope, string id, MyStackProps props) : base(scope, id, props)
        {

            var policy = new PolicyStatement(new PolicyStatementProps
            {
                Actions = new[] { "sns:Publish" },
                Resources = new[] { "*" },
                Effect = Effect.ALLOW
            });

            var preSignUp = new Function(this, "preSignUp", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("./Functions/PreSignUp/src/PreSignUp/bin/Release/netcoreapp3.1/publish"),
                Handler = "PreSignUp::PreSignUp.Function::FunctionHandler"
            });

            var defineAuthChallenge = new Function(this, "defineAuthChallenge", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("./Functions/DefineAuthChallenge/src/DefineAuthChallenge/bin/Release/netcoreapp3.1/publish"),
                Handler = "DefineAuthChallenge::DefineAuthChallenge.Function::FunctionHandler"
            });

            var createAuthChallenge = new Function(this, "createAuthChallenge", new FunctionProps
            {
                Runtime = Runtime.NODEJS_10_X,
                Code = Code.FromAsset("./Functions/CreateAuthChallengeNodeJs"),
                Handler = "create-auth-challenge.handler"
            });

            createAuthChallenge.AddToRolePolicy(policy);

            var verifyAuthChallenge = new Function(this, "verifyAuthChallenge", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("./Functions/VerifyAuthChallenge/src/VerifyAuthChallenge/bin/Release/netcoreapp3.1/publish"),
                Handler = "VerifyAuthChallenge::VerifyAuthChallenge.Function::FunctionHandler",
            });


            var userPool = new UserPool(this, $"{props.Stage}-userpool", new UserPoolProps
            {
                UserPoolName = $"{props.Stage}-userpool",
                SelfSignUpEnabled = true,
                UserVerification = new UserVerificationConfig
                {
                    EmailSubject = "Verify your email for our awesome app!",
                    EmailBody = "Hello {username}, Thanks for signing up to our awesome app! Your verification code is {####}",
                    EmailStyle = VerificationEmailStyle.CODE,
                    SmsMessage = "Hello {username}, Thanks for signing up to our awesome app! Your verification code is {####}",
                },
                UserInvitation = new UserInvitationConfig
                {
                    EmailSubject = "Invite to join our awesome app!",
                    EmailBody = "Hello {username}, you have been invited to join our awesome app! Your temporary password is {####}",
                    SmsMessage = "Hello {username}, your temporary password for our awesome app is {####}"
                },
                SignInAliases = new SignInAliases { Username = false, Email = true, Phone = true },
                AutoVerify = new AutoVerifiedAttrs { Email = true },
                SignInCaseSensitive = false,
                
                PasswordPolicy = new PasswordPolicy
                {
                    MinLength = 10,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireDigits = true,
                    RequireSymbols = true,
                    TempPasswordValidity = Duration.Days(10)
                },
                AccountRecovery = AccountRecovery.EMAIL_ONLY,

                //LambdaTriggers = new UserPoolTriggers()
                //{
                //    PreSignUp = preSignUp,
                //    DefineAuthChallenge = defineAuthChallenge,
                //    CreateAuthChallenge = createAuthChallenge,
                //    VerifyAuthChallengeResponse = verifyAuthChallenge
                //}
            });


            userPool.AddDomain("myTestDomain", new UserPoolDomainOptions
            {
                CognitoDomain = new CognitoDomainOptions
                {
                    DomainPrefix = "mytest001"
                }
            });

           
            var appClient = userPool.AddClient("app", new UserPoolClientOptions
            {
               AuthFlows = new AuthFlow
               {
                   Custom = true
               },
               OAuth = new OAuthSettings
               {
                   Flows = new OAuthFlows { AuthorizationCodeGrant = true },
                   Scopes = new[] { OAuthScope.EMAIL, OAuthScope.PROFILE, OAuthScope.PHONE, OAuthScope.OPENID },
                   CallbackUrls = new[] { "myApp://oauthredirect" },
                   LogoutUrls = new[] { "myApp://oauthredirect" }
               },
               AccessTokenValidity = Duration.Minutes(60),
               IdTokenValidity = Duration.Minutes(5),
               RefreshTokenValidity = Duration.Days(30),
            });

            var webClient = userPool.AddClient("web", new UserPoolClientOptions
            {
                AuthFlows = new AuthFlow
                {
                    UserSrp = true
                },
                OAuth = new OAuthSettings
                {
                    Flows = new OAuthFlows { AuthorizationCodeGrant = true },
                    Scopes = new[] { OAuthScope.EMAIL, OAuthScope.PROFILE, OAuthScope.PHONE, OAuthScope.OPENID },
                    CallbackUrls = new[] { "https://github.com/aws/aws-cdk" },
                    LogoutUrls = new[] { "https://github.com/aws/aws-cdk" }
                },
                AccessTokenValidity = Duration.Minutes(60),
                IdTokenValidity = Duration.Minutes(5),
                RefreshTokenValidity = Duration.Days(30)
            });
        }
    }
}
