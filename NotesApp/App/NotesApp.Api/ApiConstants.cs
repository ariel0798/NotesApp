namespace NotesApp.Api;

public static class ApiConstants
{
    public const string ContentType = "application/json";

    public static class Authentication
    {
        public const string Tag = "Auth";
        public const string BaseRoute = "auth";

        public static class EndpointNames
        {
            public const string Login = $"{BaseRoute}/login";
            public const string Register = $"{BaseRoute}/register";
            public const string RefreshToken = $"{BaseRoute}/refresh-token";
        }
    }
}