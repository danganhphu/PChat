namespace PChat.Presentation.Configurations;

public static class Router
{
    private const string root = "api";
    public const string version = "v1";
    public const string rule = root + "/" + version + "/";
    
    public static class AuthRouting
    {
        public const string Prefix = rule + "auth";

        public static class Actions
        {
            public const string Login = Prefix + "/login";
            public const string Register = Prefix + "/register";
            public const string SendResetPasswordCode = Prefix + "/sendResetPasswordCode";
            public const string ResetPassword = Prefix + "/resetPassword";
            public const string CreateTokenByRefreshToken = Prefix + "/createTokenByRefreshToken";
            public const string PostHubconnection = Prefix + "/post-hubconnection";
        }
    }
}